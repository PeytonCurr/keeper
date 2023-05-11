CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        name varchar(255),
        email varchar(255),
        picture varchar(255),
        coverImg VARCHAR(255)
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name VARCHAR(50) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(255) NOT NULL,
        views BIGINT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

ALTER TABLE keeps MODIFY COLUMN img VARCHAR(500) NOT NULL;

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name VARCHAR(50) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT FALSE,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

ALTER TABLE vaults MODIFY COLUMN img VARCHAR(500) NOT NULL;

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        vaultId INT NOT NULL,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        keepId INT NOT NULL,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

SELECT
    vk.*,
    vaultedKeep.*,
    COUNT(vkv.keepId) AS kept,
    acct.*
FROM vaultKeeps vk
    JOIN keeps vaultedKeep ON vk.keepId = vaultedKeep.id
    LEFT JOIN vaultKeeps vkv ON vkv.keepId = vaultedKeep.id
    JOIN accounts acct ON acct.id = vaultedKeep.creatorId
WHERE vk.vaultId = 88
GROUP BY (vk.id)