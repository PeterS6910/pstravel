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

export interface IImage {
	url: string;
	width?: string;
	height?: string;
	alt?: string;
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
	images: IImage[];
}

/**
 * Fetches offers with optional filters: date range, price range, locality IDs, and a maximum result limit.
 * Includes all images for each hotel.
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
                l.ParentLocalityId,
                i.Url       AS ImageUrl,
                i.Width     AS ImageWidth,
                i.Height    AS ImageHeight,
                i.Alt       AS ImageAlt
            FROM offer o
            JOIN hotel h ON o.HotelId = h.Id
            LEFT JOIN locality l ON h.LocalityId = l.Id
            LEFT JOIN images i ON h.Id = i.HotelId
            ${whereClause}
            ORDER BY o.\`From\` ASC
            ${limitClause}
        `;

		const rows: any[] = await conn.query(sql, params);

		// Group offers and collect images
		const offerMap: Record<string, IOffer> = {};
		rows.forEach(row => {
			let offer = offerMap[row.Id];
			if (!offer) {
				offer = {
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
					images: [],
				};
				offerMap[row.Id] = offer;
			}
			// Add image if exists
			if (row.ImageUrl) {
				offer.images.push({
					url: row.ImageUrl,
					width: row.ImageWidth,
					height: row.ImageHeight,
					alt: row.ImageAlt,
				});
			}
		});

		return Object.values(offerMap);
	} catch (err) {
		console.error(`${FILE}:${FUNC}: ERROR:`, err);
		throw new Error('Error fetching offers in range');
	} finally {
		conn.release();
	}
}

export async function getOffer(id: string): Promise<IOffer> {
	const conn = await dbpool.getConnection();
	try {
		const sql = `
      SELECT
        o.Id                AS OfferId,
        o.CestovkaId,
        o.HotelId,
        o.Price,
        o.Tax,
        o.TotalPrice,
        o.CurrencyId,
        o.FoodId,
        o.TransportationId,
        o.Url               AS OfferUrl,
        o.SOfferId,
        o.\`From\`,
        o.\`To\`,
        o.Length,
        o.Discount,
        h.Name              AS HotelName,
        h.Stars,
        h.Rating,
        h.RatingCount,
        l.Id                AS LocalityId,
        l.LocalityName      AS LocalityName,
        l.ParentLocalityId,
        i.Url               AS ImageUrl,
        i.Width             AS ImageWidth,
        i.Height            AS ImageHeight,
        i.Alt               AS ImageAlt
      FROM offer o
      JOIN hotel h   ON o.HotelId = h.Id
      LEFT JOIN locality l ON h.LocalityId = l.Id
      LEFT JOIN images i   ON h.Id = i.HotelId
      WHERE o.Id = ?
    `;
		const rows: any[] = await conn.query(sql, [id]);
		if (!rows.length) {
			throw new Error(`Offer with id=${id} not found`);
		}

		// Map the first row into the IOffer and accumulate images
		const first = rows[0];
		const offer: IOffer = {
			id: first.OfferId,
			cestovkaId: first.CestovkaId,
			hotelId: first.HotelId,
			hotel: {
				name: first.HotelName,
				stars: first.Stars,
				rating: first.Rating,
				ratingCount: first.RatingCount,
			},
			locality: {
				id: first.LocalityId,
				name: first.LocalityName,
				parentLocalityId: first.ParentLocalityId || undefined,
			},
			price: first.Price,
			tax: first.Tax,
			totalPrice: first.TotalPrice,
			currencyId: first.CurrencyId,
			foodId: first.FoodId,
			transportationId: first.TransportationId,
			url: first.OfferUrl,
			sOfferId: first.SOfferId,
			from: new Date(first.From),
			to: new Date(first.To),
			length: first.Length,
			discount: first.Discount,
			images: [],
		};

		// Push each image row
		rows.forEach(r => {
			if (r.ImageUrl) {
				offer.images.push({
					url: r.ImageUrl,
					width: r.ImageWidth,
					height: r.ImageHeight,
					alt: r.ImageAlt,
				} as IImage);
			}
		});

		return offer;
	} finally {
		conn.release();
	}
}

export async function getOffers(): Promise<IOffer[]> {
	return getOffersInRange(null, null, null, null, null, 20);
}

