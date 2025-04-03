import React, { useEffect, useState, useRef } from 'react';
import { LocalityCheckboxTreeNode } from '../../back/locality';
import LocalitySelectTag from './LocalitySelectTag';
import styles from './LocalityTreeSearchInput.module.css';

interface LocalityTreeSearchInputProps {
	treeData: LocalityCheckboxTreeNode[];
	closeComponentCb: () => void;
	onChange?: (selectedValues: string[]) => void;
}

const LocalityTreeSearchInput: React.FC<LocalityTreeSearchInputProps> = ({
	treeData,
	onChange,
	closeComponentCb,
}) => {
	const [selectedValues, setSelectedValues] = useState<string[]>([]);
	const [selectedValuesNormalized, setSelectedValuesNormalized] = useState<string[]>([]);
	const [expandedMap, setExpandedMap] = useState<Record<string, boolean>>({});
	const containerRef = useRef<HTMLDivElement>(null);

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

	// Normalize selected values:
	// - When all node's descendants are selected or none are selected, include the node.
	// - When only some descendants are selected, exclude the node and include its selected descendants.
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

				// If none of its descendants are selected or all of them are selected, include the node.
				if (selectedDescendantCount === 0 || selectedDescendantCount === totalDescendantCount) {
					return [node.value];
				} else {
					// If only some descendants are selected, do not include the node,
					// but include the normalized results from its children.
					let result: string[] = [];
					node.children.forEach(child => {
						result = result.concat(processNode(child));
					});
					return result;
				}
			} else {
				// If the node is not selected, process its children.
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

	// Toggle checkbox selection for a node and all its descendants.
	const handleCheckboxChange = (node: LocalityCheckboxTreeNode) => {
		setSelectedValues((prev) => {
			const isChecked = prev.includes(node.value);
			const descendantValues = getDescendantValues(node);
			let newValues: string[];
			if (isChecked) {
				// Uncheck this node and all its descendants.
				newValues = prev.filter(
					(val) => val !== node.value && !descendantValues.includes(val)
				);
			} else {
				// Check this node and all its descendants.
				newValues = Array.from(new Set([...prev, node.value, ...descendantValues]));
			}
			return newValues;
		});
	};

	// Toggle expansion state when clicking the triangle.
	const toggleNodeExpansion = (nodeValue: string) => {
		setExpandedMap((prev) => ({ ...prev, [nodeValue]: !prev[nodeValue] }));
	};

	// Whenever selectedValues changes, compute the normalized selection
	// and pass it via the onChange callback.
	useEffect(() => {
		const newNormalized = normalizeSelectedValues(treeData, selectedValues);
		setSelectedValuesNormalized(newNormalized);
		if (onChange) {
			onChange(newNormalized);
		}
	}, [selectedValues, treeData, onChange]);

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
					<label style={{ cursor: 'pointer' }}>
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
			<LocalitySelectTag
				values={selectedValuesNormalized.map((v) => ({
					value: v,
					label: getLabelForValue(v, treeData),
				}))}
				onChange={setSelectedValues}
				onClick={() => { }}
			/>
			<div className={styles.selectContainer}>{renderTreeNodes(treeData)}</div>
		</div>
	);
};

export default LocalityTreeSearchInput;

