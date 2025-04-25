import React, { useState, useEffect, useRef } from 'react';
import { DayPicker, DateRange } from 'react-day-picker';
import 'react-day-picker/dist/style.css';
import { format } from 'date-fns';
import styles from './DateRangePicker.module.css';
import { sk } from 'react-day-picker/locale';
import config from '~/config/config';

const FILE = 'DateRangePicker.tsx';

export interface ISelectedRange {
    dateRange: DateRange;
    formatedDateRange: string;
}

export interface DateRangePickerProps {
    /** Optional initial date range */
    initialRange?: DateRange;
    /** Callback when range changes */
    onRangeSelect?: (range: ISelectedRange) => void;
    closeComponentCb?: () => void;
    onClear?: () => void;
}

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
        if (initialRange) {
            let _range: ISelectedRange = {
                dateRange: initialRange,
                formatedDateRange: formatDateRange(initialRange),
            };
            onRangeSelect(_range);
        }
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

    const handleSelect = (selected: DateRange): void => {
        setRange(selected);
        let _range: ISelectedRange = {
            dateRange: initialRange,
            formatedDateRange: formatDateRange(initialRange),
        };
        onRangeSelect(_range);
    };

    let locale;
    if (config.language === 'sk') {
        locale = sk;
    } else {
        console.error(`${FILE}:${FUNC}: Unsupported language: ${config.language}`);
        locale = undefined;
    }

    const formatDate = (date: Date | undefined): string => {
        if (!date) {
            return '';
        }
        return format(date, 'PP', {locale: sk});
    }
    const formatDateRange = (range: DateRange): string => {
        if (!range.from || !range.to) {
            return '';
        }
        return `${formatDate(range.from)} - ${formatDate(range.to)}`;
    };

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
