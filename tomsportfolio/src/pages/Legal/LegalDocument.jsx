import { useState, useEffect } from 'react';
import ReactMarkdown from 'react-markdown';
import './LegalDocument.css';

const LegalDocument = ({ documentPath }) => {
  const [content, setContent] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Get the base URL from Vite's import.meta.env
    const baseUrl = import.meta.env.BASE_URL || '/';
    // Remove leading slash from documentPath if it exists
    const cleanPath = documentPath.startsWith('/') ? documentPath.slice(1) : documentPath;
    // Combine base URL with document path
    const fullPath = `${baseUrl}${cleanPath}`;

    fetch(fullPath)
      .then(res => {
        if (!res.ok) {
          throw new Error('Failed to load document');
        }
        return res.text();
      })
      .then(text => {
        setContent(text);
        setIsLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setIsLoading(false);
      });
  }, [documentPath]);

  if (isLoading) {
    return <div className="legal-loading">Loading...</div>;
  }

  if (error) {
    return <div className="legal-error">Error loading document: {error}</div>;
  }

  return (
    <div className="legal-container">
      <div className="legal-content">
        <ReactMarkdown>{content}</ReactMarkdown>
      </div>
    </div>
  );
};

export default LegalDocument; 