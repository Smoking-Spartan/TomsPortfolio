import Layout from '../components/Layout';
export default function Resume() {
    return (
        <Layout>
            <h1>Toms Resume</h1>
            <a
                href={`${import.meta.env.BASE_URL}/Thomas_Evanko_Senior_Developer_Resume_Sanitized.pdf`}
                download
                className="btn btn-primary mt-3"
                target="_blank"
                rel="noopener noreferrer"
            >
                📄 Download Resume
            </a>
            <br />
            <br />
            <div className="resume-preview d-none d-md-block">
                <iframe
                    src={`${import.meta.env.BASE_URL}/Thomas_Evanko_Senior_Developer_Resume_Sanitized.pdf`}
                    width="1250px"
                    height="1000px"
                    title="Resume Preview"
                    style={{ border: '1px solid #ccc', borderRadius: '8px' }}
                    ></iframe>
            </div>
        </Layout>
    )
}
