/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      width: {
        "1100": "1100px"
      },
      cursor: {
        pointer: "pointer",
      }
    },
  },
  plugins: [],
}

