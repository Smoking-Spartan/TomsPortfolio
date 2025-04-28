import {MainLayout} from '../components/Layout';
import NightmareCard from '../components/NightmareCard';
import { nightmares } from '../data/nightmares'; //source for the coding nightmares Ive dealt with
export default function CodingNightmares() {
    return (
        <MainLayout>
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
        </MainLayout>
    );
}