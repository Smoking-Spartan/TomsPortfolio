import { defineConfig, loadEnv } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
    const env = loadEnv(mode, process.cwd(), '');

    console.log(env.VITE_API_URL);
    return {
        plugins: [plugin()],
        server: {
            port: 11534,
            strictPort: true
        }
    };
});
