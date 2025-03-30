import React from 'react';
import styles from './LocalitySelectTag.module.css';

interface ITag {
    value: string;
    label: string;
}

interface LocalitySelectTagProps {
	values: ITag[];
	onChange: (newValues: string[]) => void;
    onClick: () => void;
}

const LocalitySelectTag: React.FC<LocalitySelectTagProps> = ({ values, onChange, onClick }) => {
	const handleDelete = (tagToRemove: ITag) => {
		const updatedValues = values.filter(tag => tag.value !== tagToRemove.value);
		onChange(updatedValues.map(tag => tag.value));
	};

	return (
		<div className={ styles.container } onClick={onClick}>
			{values.map((tag, index) => (

				<div key={ index } className={ styles.container__tag }>
					<span className={ styles.container__tagText}> { tag.label   } </span>
					<button className={ styles.container__tagDelete } onClick={(e) => {e.stopPropagation(); handleDelete(tag);}}> &times; </button>
				</div>

			))}
		</div>
	);
};

export default LocalitySelectTag;

