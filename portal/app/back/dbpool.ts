import mariadb from 'mariadb';
import 'dotenv/config';

const FILE = 'lib/dbpool.ts';

if (!process.env.DB_HOST) {
	console.error(`${FILE}: ERROR: missing DB_HOST environment variable`);
	process.exit(1);
}
if (!process.env.DB_USER) {
	console.error(`${FILE}: ERROR: missing DB_USER environment variable`);
	process.exit(1);
}
if (!process.env.DB_NAME) {
	console.error(`${FILE}: ERROR: missing DB_NAME environment variable`);
	process.exit(1);
}
if (!process.env.DB_PASSWORD) {
	console.error(`${FILE}: ERROR: missing DB_PASSWORD environment variable`);
	process.exit(1);
}

export let dbpool = mariadb.createPool({
	host: process.env.DB_HOST,
	user: process.env.DB_USER,
	database: process.env.DB_NAME,
	password: process.env.DB_PASSWORD,
	connectionLimit: 5
});

