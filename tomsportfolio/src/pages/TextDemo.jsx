import {MainLayout} from '../components/Layout';
import { QRCode } from "react-qrcode-logo";

export default function TextDemo() {
    const url = "https://your-portfolio-site.com/text-demo";
    return (
        <MainLayout>
            <h1>Text Demo</h1>
            <h3>In Progress...</h3>

            <div className="flex">
                <div className="p-4 w-1/2">
                    <h2 className="text-lg font-bold">Scan to open on your phone:</h2>
                    <QRCode value={url} size={200} />
                </div>
                <div className="p-4 w-1/2">
                    <video className="responsive-video" width="600" controls>
                        <source src={`${import.meta.env.BASE_URL}${"/media/text-demo.mp4"}`} type="video/mp4" />
                        Your browser does not support the video tag.
                    </video>
                </div>
            </div>
        </MainLayout>
    );
}