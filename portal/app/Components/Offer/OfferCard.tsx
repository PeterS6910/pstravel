import React from 'react';
import { IOffer } from '../../back/offer';
import { HiOutlineLocationMarker } from 'react-icons/hi';
import { HiClipboardList } from 'react-icons/hi';
import styles from './OfferCard.module.css';

interface OfferCardProps {
    offer: IOffer;
}

const OfferCard: React.FC<OfferCardProps> = ({ offer }) => {
    const {
        hotel,
        locality,
        price,
        discount,
        url,
        totalPrice,
        from,
        to,
    } = offer;

    return (
        <div className={styles.singleOffer}>
            <div className="imageDiv">
                <img src={url} alt={hotel.name} />
            </div>

            <div className={styles.offerInfo}>
                <h4 className={styles.hotelName}>{hotel.name}</h4>
                <span className={`${styles.location} flex`}>
                    <HiOutlineLocationMarker className="icon" />
                    <span className="name">{locality.name}</span>
                </span>

                <div className={styles.dates}>
                    <span>{from.toLocaleDateString()} - {to.toLocaleDateString()}</span>
                </div>

                <div className={`${styles.prices} flex`}>
                    <div className={ styles.basePrice }>
                        <span>Price: {price}</span>
                    </div>
                    <div className={styles.totalPrice}>
                        <h5>Total: {totalPrice}</h5>
                    </div>
                    {discount && <div className={ styles.discount }>{discount}</div>}
                </div>

                <button className='btn flex'>BOOK NOW <HiClipboardList className="icon" /></button>
            </div>
        </div>
    );
};

export default OfferCard;

