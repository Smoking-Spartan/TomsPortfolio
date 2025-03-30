
import Layout from '../components/Layout';
import React, { useEffect, useState } from "react";

export default function Home() {

    useEffect(() => {
        fetch("http://localhost:5029/Survey")
            .then((res) => res.json())
            .then((data) => {
                setMessages(data);
            })
            .catch((err) => {
                console.error("Error fetching messages:", err);
            });
    }, []);

    return (
        <Layout>
            <h1>Tom's Home</h1>
            <p>Welcome to my Portfolio!</p>
        </Layout>
    );
}