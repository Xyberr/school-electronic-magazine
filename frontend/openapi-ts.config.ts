import { defineConfig } from '@hey-api/openapi-ts';
import { loadEnv } from 'vite'

const swaggerUrl = process.env.BACKEND_SWAGGER_URL;

if (!swaggerUrl) {
    throw new Error('BACKEND_SWAGGER_URL is not defined in environment variables');
}

export default defineConfig({
  input: swaggerUrl,
  output: 'src/heyapi',
});