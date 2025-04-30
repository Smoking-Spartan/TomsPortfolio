import { useState, useEffect } from 'react';
import axios from 'axios';
import { config } from '../config';

const TestPage = () => {
    const [results, setResults] = useState({
        apiConnection: { status: 'pending', message: '' },
        databaseConnection: { status: 'pending', message: '' },
        sampleData: { status: 'pending', message: '' }
    });

    const testApiConnection = async () => {
        try {
            const response = await axios.get(`${import.meta.env.VITE_API_URL_HTTP}/test/health`);
            setResults(prev => ({
                ...prev,
                apiConnection: {
                    status: 'success',
                    message: `API is reachable. Response: ${JSON.stringify(response.data)}`
                }
            }));
        } catch (error) {
            setResults(prev => ({
                ...prev,
                apiConnection: {
                    status: 'error',
                    message: `Failed to connect to API: ${error.message}`
                }
            }));
        }
    };

    const testDatabaseConnection = async () => {
        try {
            const response = await axios.get(`${import.meta.env.VITE_API_URL_HTTP}/test/database`);
            setResults(prev => ({
                ...prev,
                databaseConnection: {
                    status: 'success',
                    message: `Database connection successful. Response: ${JSON.stringify(response.data)}`
                }
            }));
        } catch (error) {
            setResults(prev => ({
                ...prev,
                databaseConnection: {
                    status: 'error',
                    message: `Database connection failed: ${error.message}`
                }
            }));
        }
    };

    const testSampleData = async () => {
        try {
            const response = await axios.get(`${import.meta.env.VITE_API_URL_HTTP}/test/sample-data`);
            setResults(prev => ({
                ...prev,
                sampleData: {
                    status: 'success',
                    message: `Sample data retrieved: ${JSON.stringify(response.data)}`
                }
            }));
        } catch (error) {
            setResults(prev => ({
                ...prev,
                sampleData: {
                    status: 'error',
                    message: `Failed to retrieve sample data: ${error.message}`
                }
            }));
        }
    };

    const runAllTests = async () => {
        await testApiConnection();
        await testDatabaseConnection();
        await testSampleData();
    };

    return (
        <div className="p-8">
            <h1 className="text-2xl font-bold mb-6">System Test Page</h1>
            
            <div className="mb-6">
                <button 
                    onClick={runAllTests}
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 mr-4"
                >
                    Run All Tests
                </button>
                <button 
                    onClick={testApiConnection}
                    className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mr-4"
                >
                    Test API Connection
                </button>
                <button 
                    onClick={testDatabaseConnection}
                    className="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600 mr-4"
                >
                    Test Database Connection
                </button>
                <button 
                    onClick={testSampleData}
                    className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600"
                >
                    Test Sample Data
                </button>
            </div>

            <div className="space-y-4">
                {Object.entries(results).map(([key, value]) => (
                    <div key={key} className="border p-4 rounded">
                        <h2 className="font-bold capitalize mb-2">{key.replace(/([A-Z])/g, ' $1').trim()}</h2>
                        <div className={`p-2 rounded ${
                            value.status === 'success' ? 'bg-green-100' :
                            value.status === 'error' ? 'bg-red-100' :
                            'bg-gray-100'
                        }`}>
                            <p className="font-semibold">Status: {value.status}</p>
                            <p className="mt-2">{value.message}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default TestPage; 