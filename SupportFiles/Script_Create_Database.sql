CREATE SCHEMA `fileaws`;

USE `fileaws`;

CREATE TABLE `files` (
  `fileName` varchar(250) NOT NULL,
  `fileSize` bigint NOT NULL,
  `lastModified` datetime NOT NULL,
  PRIMARY KEY (`fileName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci; 