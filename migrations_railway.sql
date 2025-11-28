CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Celdas" (
    "Id" uuid NOT NULL,
    "Numero" integer NOT NULL,
    "Pabellon" text NOT NULL,
    "Capacidad" integer NOT NULL,
    CONSTRAINT "PK_Celdas" PRIMARY KEY ("Id")
);

CREATE TABLE "Guardias" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "CI" text NOT NULL,
    "Turno" text NOT NULL,
    "Rango" text NOT NULL,
    CONSTRAINT "PK_Guardias" PRIMARY KEY ("Id")
);

CREATE TABLE "Usuarios" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "CI" text NOT NULL,
    "Correo" text NOT NULL,
    "PasswordHash" text NOT NULL,
    CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id")
);

CREATE TABLE "Reclusos" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "CI" text NOT NULL,
    "FechaIngreso" timestamp with time zone NOT NULL,
    "CondenaAnios" integer NOT NULL,
    "UsuarioId" uuid NOT NULL,
    "CeldaId" uuid,
    CONSTRAINT "PK_Reclusos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Reclusos_Celdas_CeldaId" FOREIGN KEY ("CeldaId") REFERENCES "Celdas" ("Id"),
    CONSTRAINT "FK_Reclusos_Usuarios_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Expedientes" (
    "Id" uuid NOT NULL,
    "Codigo" text NOT NULL,
    "DelitoPrincipal" text NOT NULL,
    "FechaRegistro" timestamp with time zone NOT NULL,
    "ReclusoId" uuid NOT NULL,
    CONSTRAINT "PK_Expedientes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Expedientes_Reclusos_ReclusoId" FOREIGN KEY ("ReclusoId") REFERENCES "Reclusos" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Expedientes_ReclusoId" ON "Expedientes" ("ReclusoId");

CREATE INDEX "IX_Reclusos_CeldaId" ON "Reclusos" ("CeldaId");

CREATE INDEX "IX_Reclusos_UsuarioId" ON "Reclusos" ("UsuarioId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251128045401_InitialCreate', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251128054127_AddRolToUsuario', '8.0.4');

COMMIT;

