import { dbpool } from './dbpool';

const FILE = 'back/offer.ts';

interface IHotelDetails {
    name: string;
    stars: number;
    rating?: number;
    ratingCount?: number;
}

interface ILocalityDetails {
    id: string;
    name: string;
    parentLocalityId?: string;
}

export interface IOffer {
    id: string;
    cestovkaId: string;
    hotelId: string;
    hotel: IHotelDetails;
    locality: ILocalityDetails;
    price: number;
    tax: number;
    totalPrice: number;
    currencyId: string;
    foodId: string;
    transportationId: string;
    url: string;
    sOfferId: string;
    from: Date;
    to: Date;
    length: number;
    discount: string;
}

/**
 * Fetches offers with optional filters: date range, price range, locality IDs, and a maximum result limit.
 *
 * @param fromDate      start of the date range (inclusive)
 * @param toDate        end of the date range (inclusive)
 * @param minPrice      minimum total price (inclusive)
 * @param maxPrice      maximum total price (inclusive)
 * @param localityIds   array of locality IDs to filter (if non-empty)
 * @param limit         maximum number of offers to return
 */
export async function getOffersInRange(
    fromDate: Date | null,
    toDate: Date | null,
    minPrice: number | null,
    maxPrice: number | null,
    localityIds: string[] | null = [],
    limit: number | null
): Promise<IOffer[]> {
    const FUNC = 'getOffersInRange()';
    const conn = await dbpool.getConnection();

    try {
        // Build dynamic WHERE clauses
        const conditions: string[] = [];
        const params: any[] = [];

        if (fromDate && toDate) {
            conditions.push('o.`From` >= ?');
            conditions.push('o.`To` <= ?');
            params.push(fromDate, toDate);
        }

        if (minPrice != null && maxPrice != null) {
            conditions.push('o.TotalPrice BETWEEN ? AND ?');
            params.push(minPrice, maxPrice);
        }

        if (localityIds != null && localityIds.length > 0) {
            const placeholders = localityIds.map(() => '?').join(', ');
            conditions.push(`l.Id IN (${placeholders})`);
            params.push(...localityIds);
        }

        const whereClause = conditions.length
            ? `WHERE ${conditions.join('\n AND ')}`
            : '';

        // Append LIMIT clause if specified
        let limitClause = '';
        if (limit != null) {
            limitClause = ' LIMIT ?';
            params.push(limit);
        }

        const sql = `
            SELECT
                o.Id,
                o.CestovkaId,
                o.HotelId,
                o.Price,
                o.Tax,
                o.TotalPrice,
                o.CurrencyId,
                o.FoodId,
                o.TransportationId,
                o.Url,
                o.SOfferId,
                o.\`From\`,
                o.\`To\`,
                o.Length,
                o.Discount,
                h.Name AS HotelName,
                h.Stars,
                h.Rating,
                h.RatingCount,
                l.Id AS LocalityId,
                l.LocalityName,
                l.ParentLocalityId
            FROM offer o
            JOIN hotel h ON o.HotelId = h.Id
            LEFT JOIN locality l ON h.LocalityId = l.Id
            ${whereClause}
            ORDER BY o.\`From\` ASC
            ${limitClause}
        `;

        const rows: any[] = await conn.query(sql, params);

        return rows.map(row => ({
            id: row.Id,
            cestovkaId: row.CestovkaId,
            hotelId: row.HotelId,
            hotel: {
                name: row.HotelName,
                stars: row.Stars,
                rating: row.Rating,
                ratingCount: row.RatingCount,
            },
            locality: {
                id: row.LocalityId,
                name: row.LocalityName,
                parentLocalityId: row.ParentLocalityId || undefined,
            },
            price: row.Price,
            tax: row.Tax,
            totalPrice: row.TotalPrice,
            currencyId: row.CurrencyId,
            foodId: row.FoodId,
            transportationId: row.TransportationId,
            url: row.Url,
            sOfferId: row.SOfferId,
            from: new Date(row.From),
            to: new Date(row.To),
            length: row.Length,
            discount: row.Discount,
        }));
    } catch (err) {
        console.error(`${FILE}:${FUNC}: ERROR:`, err);
        throw new Error('Error fetching offers in range');
    } finally {
        conn.release();
    }
}

export	async function getOffers(): Promise<IOffer[]> {
    let offers = getOffersInRange(null, null, null, null, null, 20);
    return offers;
}
