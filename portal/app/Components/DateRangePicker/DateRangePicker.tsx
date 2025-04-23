import React, { useState, useEffect } from 'react';
import { DayPicker, DateRange } from 'react-day-picker';
import 'react-day-picker/dist/style.css';
import { format } from 'date-fns';
import styles from './DateRangePicker.module.css';

export interface DateRangePickerProps {
    /** Optional initial date range */
    initialRange?: DateRange;
    /** Callback when range changes */
    onRangeSelect?: (range: DateRange) => void;
}

const DateRangePicker: React.FC<DateRangePickerProps> = ({ initialRange, onRangeSelect }): JSX.Element => {
    if (!initialRange) {
        initialRange = { from: undefined, to: undefined };
    }
    if (!onRangeSelect) {
        onRangeSelect = () => { };
    }
    const [range, setRange] = useState<DateRange>(initialRange);

    useEffect(() => {
        if (initialRange) {
            onRangeSelect(initialRange);
        }
    }, [initialRange, onRangeSelect]);

    const handleSelect = (selected: DateRange): void => {
        setRange(selected);
        onRangeSelect(selected);
    };

    const formatDate = (date: Date | undefined): string => {
        if (!date) {
            return '-';
        }
        return format(date, 'PPP');
    }

    return (
        <div className={styles.drpContainer}>
            <h2 className={styles.drpHeading}>Select a Date Range</h2>

            <div className={styles.drpInputs}>
                <div className={styles.drpInputGroup}>
                    <label htmlFor="from">From</label>
                    <input
                        id="from"
                        type="text"
                        readOnly
                        value={formatDate(range.from)}
                        placeholder="Select start"
                        className={styles.drpInput}
                    />
                </div>
                <div className={styles.drpInputGroup}>
                    <label htmlFor="to">To</label>
                    <input
                        id="to"
                        type="text"
                        readOnly
                        value={formatDate(range.to)}
                        placeholder="Select end"
                        className={styles.drpInput}
                    />
                </div>
            </div>

            <DayPicker
                mode="range"
                required
                selected={range}
                onSelect={handleSelect}
                footer={range.from && range.to ? (
                    <p className={styles.drpFooter}>
                        Selected from <strong>{formatDate(range.from)}</strong> to <strong>{formatDate(range.to)}</strong>
                    </p>
                ) : null}
                className={styles.drpCalendar}
            />
        </div>
    );
};

export default DateRangePicker;
