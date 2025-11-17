import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
  input: 'http://localhost:5000/swagger/v1/swagger.json',
  output: 'src/heyapi',
});
