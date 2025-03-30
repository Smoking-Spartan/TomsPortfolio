import Layout from '../components/Layout';
export default function TextDemo() {
    
    return (
        <Layout>
            <h1>Text Demo</h1>
            <h3>In Progress...</h3>
            <video className="responsive-video" width="600" controls>
                <source src={`${import.meta.env.BASE_URL}${"/media/text-demo.mp4"}`} type="video/mp4" />
                Your browser does not support the video tag.
            </video>
        </Layout>
    );
}