import { dbpool } from './dbpool'; 

const FILE = 'back/locality.ts'

interface ICountry {
    id: string;
    isNajziadanejsia: boolean;
    countryName: string;
}

interface ILocality {
    id: string;
    countryId: string;
    parentLocalityId?: string;  // optional, since it may be NULL
    localityName: string;
}


async function readContries(): Promise<ICountry[]> {
    const FUNC = 'readContries()';
    const conn = await dbpool.getConnection();
    try {
        let countries: ICountry[] = [];
        const rows = await conn.query('SELECT Id, IsNajziadanejsia, CountryName  FROM country');
        for (let row of rows) {
            let country: ICountry = {
                id: row.Id,
                isNajziadanejsia: row.IsNajziadanejsia > 0,
                countryName: row.CountryName
            };
            countries.push(country);
        }
        return countries;
    } catch (err) {
        console.error(`${FILE}:${FUNC}: ERROR:`, err);
        throw new Error('error getting ids of search preferences');
    } finally {
        conn.release()
    }
}

async function getLocalities(): Promise<ILocality[]> {
    const FUNC = 'getLocalities()';
    const conn = await dbpool.getConnection();
    try {
        let localities: ILocality[] = [];
        const rows = await conn.query(
            `SELECT Id, CountryId, ParentLocalityId, LocalityName, CreatedAt, UpdatedAt, DeletedAt 
             FROM locality`
        );
        for (let row of rows) {
            let locality: ILocality = {
                id: row.Id,
                countryId: row.CountryId,
                parentLocalityId: row.ParentLocalityId ? row.ParentLocalityId : undefined,
                localityName: row.LocalityName,
            };
            localities.push(locality);
        }
        return localities;
    } catch (err) {
        console.error(`${FILE}:${FUNC}: ERROR:`, err);
        throw new Error('error getting localities');
    } finally {
        conn.release();
    }
}
