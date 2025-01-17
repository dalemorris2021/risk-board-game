DROP DATABASE IF EXISTS bots;
CREATE DATABASE bots;

DROP TABLE IF EXISTS user
DROP TABLE IF EXISTS bot
DROP TABLE IF EXISTS has_bot

CREATE TABLE user (
    username VARCHAR(255) NOT NULL,
    PRIMARY KEY (username)
);

CREATE TABLE bot (
    id INT NOT NULL,
    name VARCHAR(255).
    code_path VARCHAR(255),
    PRIMARY KEY (id)
);

CREATE TABLE has_bot (
    username VARCHAR(255) NOT NULL,
    bot_id INT NOT NULL,
    PRIMARY KEY (username, bot_id)
);

ALTER TABLE has_bot
ADD CONSTRAINT fk_username
FOREIGN KEY (username) REFERENCES user(username)
ON DELETE CASCADE

ALTER TABLE has_bot
ADD CONSTRAINT fk_bot_id
FOREIGN KEY (bot_id) REFERENCES bot(id)
ON DELETE CASCADE

-- Potential leak. What happens if a user is deleted before their bots are deleted?
