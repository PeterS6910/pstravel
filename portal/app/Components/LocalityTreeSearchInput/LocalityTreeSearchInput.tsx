import React, { useEffect, useState, useRef } from 'react';
import { LocalityCheckboxTreeNode } from '../../back/locality';
import  LocalitySelectTag  from './LocalitySelectTag';
import styles from './LocalityTreeSearchInput.module.css';

interface LocalityTreeSearchInputProps {
	treeData: LocalityCheckboxTreeNode[];
	onChange?: (selectedValues: string[]) => void;
}


const LocalityTreeSearchInput: React.FC<LocalityTreeSearchInputProps> = ({ treeData, onChange }) => {
	const [selectedValues, setSelectedValues] = useState<string[]>([]);
	const [dropdownOpen, setDropdownOpen] = useState(false);
	const containerRef = useRef<HTMLDivElement>(null);

	// Toggle the selection of a node
	const toggleSelection = (value: string) => {
		setSelectedValues((prev) => {
			if (prev.includes(value)) {
				return prev.filter((v) => v !== value);
			} else {
				return [...prev, value];
			}
		});
	};

	// Update parent if needed
	useEffect(() => {
		if (onChange) {
			onChange(selectedValues);
		}
	}, [selectedValues, onChange]);

	// Find the label corresponding to a given value by traversing the tree recursively
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

	// Render the tree recursively
	const renderTreeNodes = (nodes: LocalityCheckboxTreeNode[], level: number = 0) => {
		return nodes.map((node) => (
			<div key={node.value} style={{ paddingLeft: level * 20, margin: '4px 0' }}>
				<label style={{ cursor: 'pointer' }}>
					<input
						type="checkbox"
						checked={selectedValues.includes(node.value)}
						onChange={() => toggleSelection(node.value)}
						style={{ marginRight: 4 }}
					/>
					{node.label}
				</label>
				{node.children && node.children.length > 0 && renderTreeNodes(node.children, level + 1)}
			</div>
		));
	};

	// Close dropdown if clicking outside
	useEffect(() => {
		const handleClickOutside = (event: MouseEvent) => {
			if (dropdownOpen && containerRef.current && !containerRef.current.contains(event.target as Node)) {
				setDropdownOpen(false);
			}
		};
		document.addEventListener('mousedown', handleClickOutside);
		return () => {
			document.removeEventListener('mousedown', handleClickOutside);
		};
	}, [dropdownOpen]);

	return (
		<div ref={containerRef} className={ styles['main-container'] }>
        {/*
			<input
				type="text"
				readOnly
				value={selectedValues.map((val) => getLabelForValue(val, treeData)).join(', ')}
				placeholder="Select localities"
				onClick={() => setDropdownOpen((prev) => !prev)}
				style={{ width: '100%', padding: '6px 8px', boxSizing: 'border-box' }}
			/>
            */}
            <LocalitySelectTag values={ selectedValues.map( v => { return {value: v, label: getLabelForValue(v, treeData)}})} 
                                onChange={setSelectedValues} 
				                onClick={() => setDropdownOpen((prev) => !prev)}
            />
			{dropdownOpen && (
				<div
					style={{
						position: 'absolute',
						zIndex: 1000,
						background: 'white',
						border: '1px solid #ccc',
						padding: 10,
						marginTop: 2,
						maxHeight: 300,
						overflowY: 'auto',
						width: '100%',
					}}
				>
					{renderTreeNodes(treeData)}
				</div>
			)}
		</div>
	);
};

export default LocalityTreeSearchInput;
