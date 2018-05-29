# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## *The Reborn*

## [2.0.0] 2018-05-29

### Added
- Docker support.
- New project structure.
- Stylecop analyzers and rulesets.
- New approach to writing tests.
- Advanced and structured logging for project.

### Changed
- Completely redesigned source code.
- Combine projects [WebSocketServer](https://github.com/mpgp/WebSocketServer) and [WebApiServer](https://github.com/mpgp/WebApiServer) in [Mpgp](https://github.com/mpgp/Mpgp).
- Rename [WebApiServer](https://github.com/mpgp/WebApiServer) to [RestApiServer](https://github.com/mpgp/Mpgp/tree/master/src/Mpgp.RestApiServer).

### Deprecated
- We have refused to support multiple websocket servers. Now we will have only one websocket server. And, accordingly, removed the [API Endpoint](https://github.com/mpgp/WebApiServer/wiki/Controller.Server) for receiving a servers.

### Fixed
- Fix codestyle warnings.

### Removed
- Duplication logic in RestApiServer and WebSocketServer.