import { useLoaderData } from "@remix-run/react";
import Footer from '~/Components/Footer/Footer'
import Home from '~/Components/Home/Home'
import Main from '~/Components/Main/Main'
import Navbar from '~/Components/Navbar/Navbar'
import { LocalityCheckboxTreeNode, getLocalityCheckBoxTree } from '~/back/locality'
import { IOffer, getOffers } from '~/back/offer'

import { type MetaFunction } from "@remix-run/node";


const FILE = "routes/_index.tsx";

interface ILoaderData {
    loclityCheckboxTree: LocalityCheckboxTreeNode[],
    offers: IOffer[]
}

export const meta: MetaFunction = () => {
    return [
        { title: "New Remix App" },
        { name: "description", content: "Welcome to Remix!" },
    ];
};

export const loader = async (/*args: LoaderFunctionArgs*/) => {
    const FUNC = 'loader()';
    try {
        let loclityCheckboxTreePromise =  getLocalityCheckBoxTree();
        let offersPromise = getOffers();

        let promise = Promise.all([loclityCheckboxTreePromise, offersPromise]);
        let results = await promise;
        let loclityCheckboxTree = results[0];
        let offers = results[1];

        //console.log(`${FILE}:${FUNC}: loclityCheckboxTree:`);
        //console.dir(loclityCheckboxTree, { depth: null });
        //console.log(`${FILE}:${FUNC}: offers:`);
        //console.dir(offers, { depth: null });


        let loaderData: ILoaderData = {
            loclityCheckboxTree: loclityCheckboxTree,
            offers: offers
        };
        return loaderData;
    } catch (err) {
        console.error(`${FILE}:${FUNC}: ERROR:`, err);
        throw new Error();
    }
};

export default function Index() {
    const loaderData = useLoaderData<typeof loader>();
    const loclityCheckboxTree = loaderData.loclityCheckboxTree;
    const offers = loaderData.offers;

    console.log(`${FILE}:Index: offers:`);
    console.dir(offers, { depth: null });


    return (
        <>
            <Navbar />
            <Home loclityCheckboxTree={loclityCheckboxTree} />
            <Main offers={offers} />
            <Footer />

        </>
    );
}
