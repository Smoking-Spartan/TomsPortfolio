﻿import Layout from '../components/Layout';
import NightmareCard from '../components/NightmareCard';
import { nightmares } from '../data/nightmares';
export default function CodingNightmares() {
    return (
        <Layout>
            <h1 className="">Tom's Coding Nightmares</h1>
            {nightmares.map(n => (
                <NightmareCard key={n.id}
                    title={n.title}
                    summary={n.summary}
                    solution={n.solution}>

                    {n.content.map((p, i) => (
                        <p key={i}>{p}</p>
                    ))}

                </NightmareCard>
            ))}
        </Layout>
    );
}