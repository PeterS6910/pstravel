import React from 'react';
import { IOffer } from '../../back/offer';
import { HiOutlineLocationMarker } from 'react-icons/hi';
import styles from './OfferDetail.module.css';

interface OfferDetailProps {
    offer: IOffer;
}

const OfferDetail: React.FC<OfferDetailProps> = ({ offer }) => {
    const {
        id,
        cestovkaId,
        sOfferId,
        hotel,
        locality,
        from,
        to,
        length,
        price,
        tax,
        totalPrice,
        discount,
        currencyId,
        foodId,
        transportationId,
        images,
    } = offer;

    return (
        <div className={styles.offerDetail}>
            {/* Image gallery */}
            <div className={styles.imageGallery}>
                {images.map((img, idx) => (
                    <div key={idx} className={styles.imageItem}>
                        <img
                            src={img.url}
                            alt={img.alt || hotel.name}
                            className={styles.image}
                        />
                    </div>
                ))}
            </div>

            {/* Offer details */}
            <div className={styles.details}>
                <h2 className={styles.hotelName}>{hotel.name}</h2>

                <div className={styles.location}>
                    <HiOutlineLocationMarker className={styles.icon} />
                    <span>{locality.name}</span>
                </div>

                <div className={styles.meta}>
                    <span>Offer ID: {id}</span>
                    <span>Supplier: {cestovkaId}</span>
                    <span>Supplier Offer ID: {sOfferId}</span>
                </div>

                <div className={styles.dates}>
                    <span>
                        <strong>From:</strong> {from.toLocaleDateString()}
                    </span>
                    <span>
                        <strong>To:</strong> {to.toLocaleDateString()}
                    </span>
                    <span>
                        <strong>Length:</strong> {length} days
                    </span>
                </div>

                <div className={styles.prices}>
                    <span>
                        <strong>Price:</strong> {price} {currencyId}
                    </span>
                    <span>
                        <strong>Tax:</strong> {tax} {currencyId}
                    </span>
                    <span>
                        <strong>Total:</strong> {totalPrice} {currencyId}
                    </span>
                    {discount && (
                        <span className={styles.discount}>
                            <strong>Discount:</strong> {discount}%
                        </span>
                    )}
                </div>

                <div className={styles.misc}>
                    <span>
                        <strong>Stars:</strong> {hotel.stars}
                    </span>
                    {hotel.rating != null && (
                        <span>
                            <strong>Rating:</strong> {hotel.rating} ({hotel.ratingCount} reviews)
                        </span>
                    )}
                    <span>
                        <strong>Food ID:</strong> {foodId}
                    </span>
                    <span>
                        <strong>Transport ID:</strong> {transportationId}
                    </span>
                </div>

                <button className={styles.bookBtn}>Book Now</button>
            </div>
        </div>
    );
};

export default OfferDetail;

