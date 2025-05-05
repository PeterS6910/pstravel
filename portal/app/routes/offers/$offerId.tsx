import type { LoaderFunction } from '@remix-run/node';
import { useLoaderData } from '@remix-run/react';

import OfferDetail from '~/Components/OfferDetail/OfferDetail';
import { getOffer } from '~/back/offer';
import type { IOffer } from '~/back/offer';

type LoaderData = {
    offer: IOffer;
};

export const loader: LoaderFunction = async ({ params }) => {
    const id = params.offerId;
    if (!id) {
        throw new Response('Offer not found', { status: 404 });
    }
    const offer = await getOffer(id);
    let loaderData: LoaderData = { offer };
    return loaderData;
};

export default function OfferDetailPage() {
    const { offer } = useLoaderData<LoaderData>();
    return <OfferDetail offer={offer} />;
}

