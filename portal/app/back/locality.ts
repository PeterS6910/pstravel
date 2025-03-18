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


async function getContries(): Promise<ICountry[]> {
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

export interface LocalityCheckboxTreeNode {
	value: string;
	label: string;
    isCountry: boolean;
	children: LocalityCheckboxTreeNode[];
}

export async function getLocalityCheckBoxTree(): Promise<LocalityCheckboxTreeNode[]> {
	
	const countries = await getContries();
	const localities = await getLocalities();

	
	function buildLocalityTreeMode( parentLocality: ILocality, localities: ILocality[]): LocalityCheckboxTreeNode {

        let node: LocalityCheckboxTreeNode = {
            value: parentLocality.id,
            label: parentLocality.localityName,
            isCountry: false,
            children: []
        }

		let children = localities
			.filter(locality => locality.parentLocalityId === parentLocality.id)
			.map(locality => {

				const child = buildLocalityTreeMode(locality, localities);
                return child;    

			});

        node.children = children;
        return node;
	}

	
	const tree: LocalityCheckboxTreeNode[] = countries.map(country => {

		const localitiesForCountry = localities.filter(
			locality => locality.countryId === country.id
		);

        let node: LocalityCheckboxTreeNode = {
            value: country.id,
            label: country.countryName,
            isCountry: true,
            children: []
        };

        let children = localitiesForCountry.map(locality => {
            return buildLocalityTreeMode(locality, localities);
        });

        node.children = children;
        return node;

	});

	return tree;
}

