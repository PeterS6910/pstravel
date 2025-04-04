import React, { useEffect, useState, useRef } from 'react';
import { LocalityCheckboxTreeNode } from '../../back/locality';
import LocalitySelectTag from './LocalitySelectTag';
import styles from './LocalityTreeSearchInput.module.css';
import deburr from 'lodash.deburr';

interface LocalityTreeSearchInputProps {
    treeData: LocalityCheckboxTreeNode[];
    selectedValuesInit: string[];
    closeComponentCb: () => void;
    onChange?: (selectedValues: string[]) => void;
}

const printTreeData = (nodes: LocalityCheckboxTreeNode[], indent: string = ''): void => {
    nodes.forEach(node => {
        console.log(`${indent}Node: ${node.label} (value: ${node.value})`);
        if (node.children && node.children.length > 0) {
            printTreeData(node.children, indent + '  ');
        }
    });
};



const LocalityTreeSearchInput: React.FC<LocalityTreeSearchInputProps> = ({
    treeData,
    selectedValuesInit,
    onChange,
    closeComponentCb,
}) => {
    const [selectedValues, setSelectedValues] = useState<string[]>(selectedValuesInit);
    const [selectedValuesNormalized, setSelectedValuesNormalized] = useState<string[]>([]);
    const [expandedMap, setExpandedMap] = useState<Record<string, boolean>>({});
    const [searchText, setSearchText] = useState('');
    const containerRef = useRef<HTMLDivElement>(null);

    //printTreeData(treeData);

    // Recursively gather all descendant values of a node.
    const getDescendantValues = (node: LocalityCheckboxTreeNode): string[] => {
        let values: string[] = [];
        if (node.children && node.children.length > 0) {
            node.children.forEach((child) => {
                values.push(child.value);
                values = values.concat(getDescendantValues(child));
            });
        }
        return values;
    };

    // Normalize selected values for tree mode only.
    // When in search mode, we simply use the selectedValues as-is.
    const normalizeSelectedValues = (
        nodes: LocalityCheckboxTreeNode[],
        selected: string[]
    ): string[] => {
        const selectedSet = new Set(selected);

        const processNode = (node: LocalityCheckboxTreeNode): string[] => {
            if (selectedSet.has(node.value)) {
                // If the node is a leaf, simply include it.
                if (!node.children || node.children.length === 0) {
                    return [node.value];
                }
                // Get all descendant values.
                const descendantValues = getDescendantValues(node);
                const selectedDescendantCount = descendantValues.filter(val => selectedSet.has(val)).length;
                const totalDescendantCount = descendantValues.length;

                // If none or all descendants are selected, include the node.
                if (selectedDescendantCount === 0 || selectedDescendantCount === totalDescendantCount) {
                    return [node.value];
                } else {
                    // Otherwise, process children.
                    let result: string[] = [];
                    node.children.forEach(child => {
                        result = result.concat(processNode(child));
                    });
                    return result;
                }
            } else {
                // If not selected, process children.
                let result: string[] = [];
                if (node.children && node.children.length > 0) {
                    node.children.forEach(child => {
                        result = result.concat(processNode(child));
                    });
                }
                return result;
            }
        };

        let normalized: string[] = [];
        nodes.forEach(node => {
            normalized = normalized.concat(processNode(node));
        });
        return normalized;
    };

    // Toggle checkbox selection for a node.
    // In tree mode (when searchText is empty), we toggle the node and all its descendants.
    // In search mode, we simply toggle the node's value.
    const handleCheckboxChange = (node: LocalityCheckboxTreeNode) => {
        if (searchText) {
            // Search mode: simply toggle this node's selection.
            setSelectedValues((prev) => {
                return prev.includes(node.value)
                    ? prev.filter(val => val !== node.value)
                    : [...prev, node.value];
            });
        } else {
            // Tree mode: toggle the node and all its descendants.
            setSelectedValues((prev) => {
                const isChecked = prev.includes(node.value);
                const descendantValues = getDescendantValues(node);
                let newValues: string[];
                if (isChecked) {
                    newValues = prev.filter(
                        (val) => val !== node.value && !descendantValues.includes(val)
                    );
                } else {
                    newValues = Array.from(new Set([...prev, node.value, ...descendantValues]));
                }
                return newValues;
            });
        }
    };

    // Toggle expansion state when clicking the triangle.
    const toggleNodeExpansion = (nodeValue: string) => {
        setExpandedMap((prev) => ({ ...prev, [nodeValue]: !prev[nodeValue] }));
    };

    // Update normalized selection.
    useEffect(() => {
        if (searchText) {
            // In search mode, bypass normalization.
            setSelectedValuesNormalized(selectedValues);
            if (onChange) {
                onChange(selectedValues);
            }
        } else {
            const newNormalized = normalizeSelectedValues(treeData, selectedValues);
            setSelectedValuesNormalized(newNormalized);
            if (onChange) {
                onChange(newNormalized);
            }
        }
    }, [selectedValues, treeData, onChange, searchText]);

    // Recursively find the label for a given value.
    const getLabelForValue = (value: string, nodes: LocalityCheckboxTreeNode[]): string => {
        for (let node of nodes) {
            if (node.value === value) return node.label;
            if (node.children && node.children.length > 0) {
                const result = getLabelForValue(value, node.children);
                if (result) return result;
            }
        }
        return '';
    };

    // Render the tree recursively.
    const renderTreeNodes = (nodes: LocalityCheckboxTreeNode[], level: number = 0) => {
        return nodes.map((node) => (
            <div key={node.value} style={{ paddingLeft: level * 20, margin: '4px 0' }}>
                <div style={{ display: 'flex', alignItems: 'center' }}>
                    <label style={{ cursor: 'pointer', padding: '0px' }}>
                        <input
                            type="checkbox"
                            checked={selectedValues.includes(node.value)}
                            onChange={() => handleCheckboxChange(node)}
                            style={{ marginRight: 4 }}
                        />
                        {node.label}
                    </label>
                    {node.children && node.children.length > 0 && (
                        <span
                            onClick={() => toggleNodeExpansion(node.value)}
                            style={{ marginLeft: 'auto', cursor: 'pointer', color: 'blue' }}
                        >
                            {expandedMap[node.value] ? '▼' : '►'}
                        </span>
                    )}
                </div>
                {node.children && node.children.length > 0 && expandedMap[node.value] && (
                    <div>{renderTreeNodes(node.children, level + 1)}</div>
                )}
            </div>
        ));
    };


    const filterTree = (nodes: LocalityCheckboxTreeNode[], query: string): LocalityCheckboxTreeNode[] => {
        let result: LocalityCheckboxTreeNode[] = [];
        const normalizedQuery = deburr(query.toLowerCase());

        nodes.forEach(node => {
            const normalizedLabel = deburr(node.label.toLowerCase());
            if (normalizedLabel.includes(normalizedQuery)) {
                result.push(node);
            }
            if (node.children && node.children.length > 0) {
                result = result.concat(filterTree(node.children, query));
            }
        });

        // Remove duplicates based on node.value
        const uniqueNodes = new Map<string, LocalityCheckboxTreeNode>();
        result.forEach(node => {
            uniqueNodes.set(node.value, node);
        });

        return Array.from(uniqueNodes.values());
    };


    // Close dropdown if clicking outside.
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (containerRef.current && !containerRef.current.contains(event.target as Node)) {
                closeComponentCb();
            }
        };
        document.addEventListener('click', handleClickOutside, true);
        return () => {
            document.removeEventListener('click', handleClickOutside);
        };
    }, [closeComponentCb]);

    return (
        <div ref={containerRef} className={styles.mainContainer}>
            <input
                type="text"
                placeholder="Search..."
                value={searchText}
                onChange={(e) => {
                    // When typing in search, clear previous selections.
                    setSearchText(e.target.value);
                    setSelectedValues([]);
                }}
                className={styles.searchInput}
            />
            {selectedValuesNormalized.length > 0 && <LocalitySelectTag
                values={selectedValuesNormalized.map((v) => ({
                    value: v,
                    label: getLabelForValue(v, treeData),
                }))}
                onChange={setSelectedValues}
                onClick={() => { }}
            />}
            {searchText ? (
                <div className={styles.selectContainer}>
                    {filterTree(treeData, searchText).map((node) => (
                        <div key={node.value} style={{ margin: '4px 0' }}>
                            <label style={{ cursor: 'pointer' }}>
                                <input
                                    type="checkbox"
                                    checked={selectedValues.includes(node.value)}
                                    onChange={() => handleCheckboxChange(node)}
                                    style={{ marginRight: 4 }}
                                />
                                {node.label}
                            </label>
                        </div>
                    ))}
                </div>
            ) : (
                <div className={styles.selectContainer}>
                    {renderTreeNodes(treeData)}
                </div>
            )}
            <div className={styles.buttonsContainer}>
                <button 
                    className={styles.okButton}
                    type="button" onClick={closeComponentCb}>
                    Ok
                </button>
            </div>
        </div>
    );
};

export default LocalityTreeSearchInput;
