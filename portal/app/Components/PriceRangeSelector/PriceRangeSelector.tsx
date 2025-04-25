import React, { FC, useState, useEffect } from 'react';

interface PriceRangeSelectorProps {
    min?: number;
    max?: number;
    initial?: number;
    onChange?: (value: number) => void;
}

const PriceRangeSelector: FC<PriceRangeSelectorProps> = ({
    min = 1000,
    max = 5000,
    initial = 5000,
    onChange,
}) => {
    const [price, setPrice] = useState<number>(initial);

    useEffect(() => {
        if (onChange) {
            onChange(price);
        }
    }, [price, onChange]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPrice(Number(e.target.value));
    };

    return (
        <input
            id="priceRange"
            type="range"
            min={min}
            max={max}
            value={price}
            onChange={handleChange}
            className="w-full"
        />
    );
};

export default PriceRangeSelector;

