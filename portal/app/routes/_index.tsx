import { useLoaderData } from "@remix-run/react";
import Footer from '~/Components/Footer/Footer'
import Home from '~/Components/Home/Home'
import Main from '~/Components/Main/Main'
import Navbar from '~/Components/Navbar/Navbar'
import { LocalityCheckboxTreeNode, getLocalityCheckBoxTree } from '~/back/locality'
import InputLocalityTreeSearch from '~/Components/InputLocalityTreeSearch/InputLocalityTreeSearch'

import { type MetaFunction } from "@remix-run/node";


const FILE = "routes/_index.tsx";

interface ILoaderData {
    loclityCheckboxTree: LocalityCheckboxTreeNode[]
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
        let loclityCheckboxTree = await getLocalityCheckBoxTree();
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        console.log(`${FILE}:${FUNC}: @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ loclityCheckboxTree:`, loclityCheckboxTree);
        console.dir(loclityCheckboxTree, { depth: null });
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        let loaderData: ILoaderData = {
            loclityCheckboxTree: loclityCheckboxTree
        };
        return loaderData;
    } catch (err) {
        console.error(`${FILE}:${FUNC}: ERROR:`, err);
        throw new Error();
    }
};

export default function Index() {
    const loaderData = useLoaderData<typeof loader>();
    const nodes = loaderData.loclityCheckboxTree;


    return (
        <>
            <Navbar />
            <Home />
            <Main />
            <Footer />
            <InputLocalityTreeSearch
                treeData={nodes}
                onChange={(selectedValues: string[]) => {
                    console.log('selectedValues:', selectedValues);
                }}
            />

        </>
    );
}
