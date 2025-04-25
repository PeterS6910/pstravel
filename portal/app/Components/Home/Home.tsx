import { useState } from 'react'
import React from 'react';
import './home.css'
import { GrLocation, GrCalendar  } from 'react-icons/gr/index.js'
import { HiFilter } from 'react-icons/hi/index.js'
import { FiFacebook } from 'react-icons/fi/index.js'
import { AiOutlineInstagram } from 'react-icons/ai/index.js'
import { SiTripadvisor } from 'react-icons/si/index.js'
import { BsListTask } from 'react-icons/bs/index.js'
import { TbApps } from 'react-icons/tb/index.js'

import LocalityTreeSearchInput from '~/Components/LocalityTreeSearchInput/LocalityTreeSearchInput'
import { LocalityCheckboxTreeNode } from '~/back/locality'

import DateRangePicker, { ISelectedRange } from '~/Components/DateRangePicker/DateRangePicker'
import { DateRange } from 'react-day-picker';

interface HomeProps {
    loclityCheckboxTree: LocalityCheckboxTreeNode[];
}

const Home: React.FC<HomeProps> = ({ loclityCheckboxTree }) => {

    const [displayLocalityTree, setDisplayLocalityTree] = useState(false);
    const [selectedLocalities, setSelectedLocalities] = useState<string[]>([]);

    const [displayDayPicker, setDisplayDayPicker] = useState(false);

    const [selectedDateRange, setSelectedDateRange] = useState<ISelectedRange>({
        dateRange: { from: undefined, to: undefined },
        formatedDateRange: '',
    });



    function localitiesToString(localities: string[]): string {
        return localities.map((locality) => {
            const localityObj = loclityCheckboxTree.find((item) => item.value === locality);
            return localityObj ? localityObj.label : locality;
        }).join(', ') || '';
    }

    const initialDateRange: DateRange = {
        from: new Date(new Date().setDate(new Date().getDate() + 1)),
        to: new Date(new Date().setDate(new Date().getDate() + 7)),
    };

    const handleDateRangeSelect = (range: ISelectedRange) => {
        console.log('@@@@@@@@@@@@@@@@@@@@@ Selected date range:', range);
        setSelectedDateRange(range);
    };

    return (
        <section id='home' className='home'>
            <div className="overlay"></div>
            <video autoPlay loop muted >
                <source src="Assets/video.mp4" type="video/mp4" />
            </video>

            <div className="homeContent container">
                <div className="textDiv">
                    <span className="smallText">
                        Our Packages
                    </span>
                    <h1 className="homeTitle">
                        Search your Holiday
                    </h1>
                </div>

                <div className="cardDiv grid">
                    <div className="destinationInput">
                        <div>
                            <label htmlFor="city">Search your destination:</label>
                            <div className="input flex">
                                {/*<input type="text" placeholder='Enter name here...' onClick={() => setDisplayLocalityTree(!displayLocalityTree)} value={localitiesToString(selectedLocalities)} />*/}
                                <input type="text" readOnly placeholder='Enter name here...' onClick={() => setDisplayLocalityTree(!displayLocalityTree)} value={localitiesToString(selectedLocalities)} />
                                <GrLocation className="icon" onClick={() => setDisplayLocalityTree(!displayLocalityTree)} />
                            </div>
                        </div>
                        {displayLocalityTree && <div className="localitySearchDialog">
                            <LocalityTreeSearchInput
                                treeData={loclityCheckboxTree}
                                selectedValuesInit={selectedLocalities}
                                closeComponentCb={() => setDisplayLocalityTree(false)}
                                onChange={(selectedValues: string[]) => {
                                    console.log('selectedValues:', selectedValues);
                                    setSelectedLocalities(selectedValues);
                                }}
                                onClear={() => { setSelectedLocalities([]); setDisplayLocalityTree(false) }}
                            />
                        </div>}
                    </div>


                    <div className="dateInput">
                        <label htmlFor="city">Select your date:</label>
                        <div className="input flex">
                        <input type="text" readOnly placeholder='Select the date range ...' onClick={() => setDisplayDayPicker(!displayDayPicker)} value={selectedDateRange.formatedDateRange} />
                        <GrCalendar className="icon" onClick={() => setDisplayDayPicker(!displayDayPicker)} />
                        {displayDayPicker &&  <div className="dayRangePickerDialog">
                            <DateRangePicker 
                                initialRange={initialDateRange} 
                                onRangeSelect={handleDateRangeSelect} 
                                closeComponentCb={() => setDisplayDayPicker(false)}
                            />
                        </div>}
                    </div>
                    </div>

                    <div className="priceInput">
                        <div className="label_total flex">
                            <label htmlFor="city">Max price:</label>
                            <h3 className="total">$5000</h3>
                        </div>
                        <div className="input flex">
                            <input type="range" max="5000" min="1000" />
                        </div>
                    </div>

                    <div className="searchOptions flex">
                        <HiFilter className="icon" />
                        <span>MORE FILTERS</span>
                    </div>
                </div>

                {/*<div data-aos="fade-up" className="homeFooterIcons flex"> */}
                <div className="homeFooterIcons flex">
                    <div className="rightIcons">
                        <FiFacebook className="icon" />
                        <AiOutlineInstagram className="icon" />
                        <SiTripadvisor className="icon" />
                    </div>
                    <div className="leftIcons">
                        <BsListTask className="icon" />
                        <TbApps className="icon" />
                    </div>
                </div>
            </div>
        </section>
    )
}

export default Home
