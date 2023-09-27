const PROXY_CONFIG = [
  {
    context: [
      "/Auth/login",
    ],
    target: "https://localhost:7098",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
