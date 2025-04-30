import { defineConfig, loadEnv } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig(({ command, mode }) => {
    // Load env file based on `mode` in the current working directory.
    // Set the third parameter to '' to load all env regardless of the `VITE_` prefix.
    const env = loadEnv(mode, process.cwd(), '');

    return {
        plugins: [plugin()],
        server: {
            port: 11534,
            strictPort: true
        },
        define: {
            // Make env variables available to the app
            'process.env.VITE_API_URL_HTTP': JSON.stringify(env.VITE_API_URL_HTTP)
        }
    };
});
