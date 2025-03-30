import Layout from '../components/Layout';
import { hobbies } from '../data/hobbies';
import HobbyCard from '../components/HobbyCard';

export default function Hobbies() {
    return (
        <Layout>
            <h1>Hobbies Page</h1>
            <br />
            {hobbies.map((hobby) => (
                <HobbyCard key={hobby.title} hobby={hobby} />
            ))}
        </Layout>
    );
}
