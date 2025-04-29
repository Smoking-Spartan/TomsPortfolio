import {MainLayout} from '../components/Layout';
import React, { useEffect, useState } from "react";

export default function Home() {
    return (
        <MainLayout>
            <h1>Tom Evanko</h1>
            <h3>Full-Stack Developer | Systems Stabilizer | Performance Optimizer</h3>
            <p>Building scalable systems, modernizing legacy code, and solving the problems others run from.</p>

            <h3>About</h3>
            <p>
            I'm a Full-Stack Developer with 7+ years of experience transforming unstable, outdated systems into fast, reliable platforms.
            </p>

            <p>I specialize in:</p>
            <div className="list-container">
                <ul className="checkBulletList">
                    <li><b>API Integrations</b> (Nextiva, Zoom, SMS platforms)</li>
                    <li><b>Legacy System Modernization</b> (.NET 4 → Stabilized and expanded)</li>
                    <li><b>Database Optimization</b> (Reduced multi-day SQL waits to under 3 hours)</li>
                    <li><b>Infrastructure & Server Setup</b> (Built full QA environment from scratch)</li>
                    <li><b>Real-Time Application Development</b> (Text-messaging opt-ins, live survey feedback)</li>
                </ul>
            </div>
            <p>
                Whether it's cutting a 10-minute report down to under a second, saving clients from churn with real-time demos, or stabilizing production systems that used to require server restarts every two weeks — I deliver real, measurable impact across the stack.
            </p>

            <h3>Highlight Projects</h3>

            <h5>Full Phone System Migrations</h5>
            <p>Integrated Nextiva and Zoom APIs across 3 web applications, enabling live call listening and click-to-dial functionality.</p>

            <h5>Real-Time SMS Feedback System</h5>
            <p>Designed and built an SMS opt-in and survey demo in under 48 hours, directly saving major client accounts.</p>

            <h5>QA Server Buildout</h5>
            <p>Independently configured a full QA environment (.NET 4, AngularJS 1.0, SQL Server) with no documentation to mirror production reliably.</p>

            <h5>Database and Import Optimizations</h5>
            <p>Reduced SQL wait times from days to hours, and slashed import processing times by 75%.</p>

            <h5>System Stabilization</h5>
            <p>Stopped weekly server restarts by diagnosing and correcting app pool, IIS, and database stability issues.</p>
        </MainLayout>
    );
}