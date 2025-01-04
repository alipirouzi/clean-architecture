CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;
INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250103134449_Initial', '9.0.0');

CREATE TABLE outbox_messages (
    id uuid NOT NULL,
    type text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    content text NOT NULL,
    modified_at_utc timestamp with time zone,
    CONSTRAINT pk_outbox_messages PRIMARY KEY (id)
);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250103140415_adding_OutboxMessage', '9.0.0');

ALTER TABLE outbox_messages RENAME COLUMN modified_at_utc TO processed_at_utc;

ALTER TABLE outbox_messages ADD error text NOT NULL DEFAULT '';

CREATE TABLE orders (
    id uuid NOT NULL,
    order_number integer NOT NULL,
    name text NOT NULL,
    created_at_utc timestamp with time zone NOT NULL,
    CONSTRAINT pk_orders PRIMARY KEY (id)
);

CREATE INDEX ix_orders_created_at_utc ON orders (created_at_utc);

CREATE INDEX ix_orders_order_number ON orders (order_number);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250103160439_modified_outbox_messages', '9.0.0');

COMMIT;

