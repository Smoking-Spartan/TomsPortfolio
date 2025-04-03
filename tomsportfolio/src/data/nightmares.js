export const nightmares = [
    {
        id: 'downSystem',
        title: 'Symptoms of A Down System',
        summary: 'A previous employers solution for dealing with an unresponsive website for clients and internal users.',
        solution:
            'Not easy, and hard to pinpoint what exactly resolved the system performance. I was able to get the system to stop with multiple database related fixes and optimizations',
        content: [
            "Hired on full time for this company, their first internal dev hire. Always have used contractors to create and deal with their internal systems.",
            "Their lead business person hands me this word document and explains to me what needs to be done if employees or clients complained about spinning when using the websites.",
            "So it went something like this, if there was reports of spinning, the first step was to recyle the app pool in IIS, meaning you had to remote into the production application server and recyle them.",
            "If that didn't work, restart IIS",
            "If that didn't work, restart SQL Server on the database servers",
            "If everyone was still complaining? The Hail Mary, restart all the servers."
        ]
    },
    {
        id: 'demo-crunch',
        title: '🔥 The 2AM Text Demo Fire Drill',
        summary: '48 hours. No specs. No QA. Just pressure and adrenaline.',
        solution:
            'Built a full opt-in flow and chained survey delivery in React with SMS triggers. Saved the client, stabilized the team.',
        content: [
            "It started on a Thursday when Doug mentioned — casually — that a client was threatening to walk.",
            "No requirements. No API documentation. No staging environment. Just the words: 'We need this demo live by Monday.'",
            "I locked in. Built a React frontend for opt-ins. Wired up Vonage SMS. Chained to the survey system. Pulled completion data. Verified messages.",
            "Worked through the weekend. Slept 4 hours across 2 days. Got it done. And the client stayed."
        ]
    },
    {
        id: 'ghost-bug',
        title: '👻 The Ghost Bug',
        summary: 'A phantom bug that only appeared on Tuesdays when Jill wasn’t watching.',
        solution:
            'Tracked it to stale cache after an inconsistent deployment. Added QA-verified logs and flushed prod cache.',
        content: [
            "Users kept reporting a weird bug that we could never replicate. QA swore it was fake. I swore I was losing my mind.",
            "After combing logs and checking every build artifact, I realized prod had stale JS files that weren’t being overwritten on deploy.",
            "It only triggered on edge-case input *and* when hitting the old cached script. Nightmare.",
            "Flushed the CDN. Added deployment logging. QA added cache-buster checks. Never happened again."
        ]
    },
    {
        id: 'rogue-sql',
        title: '💣 The Rogue SQL Script',
        summary: 'When a contractor ran a “quick fix” and nuked 200 client records.',
        solution:
            'Restored backup, added role-based permission checks, and wrote a SQL logging trigger for live auditing.',
        content: [
            "This one still gives me trust issues. A contractor thought he was in dev — he was in prod.",
            "Ran a script to clean up a test record, but forgot the `WHERE` clause. Deleted client data from 7 different orgs.",
            "Thankfully I had a backup from that morning and restored it. Still lost about an hour’s worth of live data.",
            "After that, I wrote role-based DB access checks and a live auditing trigger that logs all delete/update operations."
        ]
    },
    {
        id: 'databaseContractor',
        title: '💣 We Let Our Contractor DBA go, Can you Maintain the databases?',
        summary: 'Letting go of our Conrtractor DBA, I had to recreate the datbase backup process, maintaining the tables, indexes, and monitor solar winds',
        solution:
            'Restored backup, added role-based permission checks, and wrote a SQL logging trigger for live auditing.',
        content: [
            "This one still gives me trust issues. A contractor thought he was in dev — he was in prod.",
            "Ran a script to clean up a test record, but forgot the `WHERE` clause. Deleted client data from 7 different orgs.",
            "Thankfully I had a backup from that morning and restored it. Still lost about an hour’s worth of live data.",
            "After that, I wrote role-based DB access checks and a live auditing trigger that logs all delete/update operations."
        ]
    }
];