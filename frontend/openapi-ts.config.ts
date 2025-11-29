import { defineConfig } from '@hey-api/openapi-ts';
import { loadEnv } from 'vite'

const swaggerUrl = loadEnv('development', './').VITE_BACKEND_SWAGGER
if (!swaggerUrl) {
    throw new Error('API_SWAGGER_URL is not defined in environment variables');
}

export default defineConfig({
  input: swaggerUrl,
  output: 'src/heyapi',
});