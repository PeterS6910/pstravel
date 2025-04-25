import React, { useState, useEffect, useRef } from 'react';
import { DayPicker, DateRange } from 'react-day-picker';
import 'react-day-picker/dist/style.css';
import { format } from 'date-fns';
import styles from './DateRangePicker.module.css';
import { sk } from 'react-day-picker/locale';
import config from '~/config/config';

const FILE = 'DateRangePicker.tsx';


export interface DateRangePickerProps {
    /** Optional initial date range */
    initialRange?: DateRange;
    /** Callback when range changes */
    onRangeSelect?: (range: DateRange) => void;
    closeComponentCb?: () => void;
    onClear?: () => void;
}

function formatDate(date: Date | undefined): string {
    if (!date) {
        return '';
    }
    return format(date, 'PP', {locale: sk});
}

export function formatDateRange(range: DateRange): string {
    if (!range.from || !range.to) {
        return '';
    }
    return `${formatDate(range.from)} - ${formatDate(range.to)}`;
};

const DateRangePicker: React.FC<DateRangePickerProps> = ({ initialRange, onRangeSelect, closeComponentCb, onClear }): JSX.Element => {
    const FUNC = 'DateRangePicker()';
    if (!initialRange) {
        initialRange = { from: undefined, to: undefined };
    }
    if (!onRangeSelect) {
        onRangeSelect = () => { };
    }
    if (!closeComponentCb) {
        closeComponentCb = () => { };
    }
    if (!onClear) {
        onClear = () => { };
    }
    const [range, setRange] = useState<DateRange>(initialRange);
    const [isMounted, setIsMounted] = useState(false);

    const containerRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        onRangeSelect(initialRange);
    }, [initialRange, onRangeSelect]);

    useEffect(() => {
        setIsMounted(true);
    }, []);

    // Close if clicking outside.
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

    const handleSelect = (selectedRange: DateRange): void => {
        setRange(selectedRange);
        onRangeSelect(selectedRange);
    };

    let locale;
    if (config.language === 'sk') {
        locale = sk;
    } else {
        console.error(`${FILE}:${FUNC}: Unsupported language: ${config.language}`);
        locale = undefined;
    }

    return (
        <div ref={containerRef} className={styles.container}>
            {range.from && range.to ? (
                <div className={styles.drpHeader}>
                    {formatDateRange(range)}
                </div>
            ) : null}

            {isMounted && <div> <DayPicker
                mode="range"
                required
                defaultMonth={range.from}
                selected={range}
                onSelect={handleSelect}
            /></div>}
            <div className={styles.buttonsContainer}>
                <button
                    className={styles.okButton}
                    type="button" onClick={closeComponentCb}>
                    Ok
                </button>

                <button
                    className={styles.cancelButton}
                    type="button" onClick={onClear}>
                    Clear
                </button>
            </div>
        </div>
    );
};

export default DateRangePicker;
