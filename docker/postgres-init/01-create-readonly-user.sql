-- Create the read-only user for report query execution
CREATE USER ro_user WITH PASSWORD 'ro_password_dev';

-- Grant connect access to the database
GRANT CONNECT ON DATABASE "ReportBuilder" TO ro_user;

-- Grant usage on the public schema
GRANT USAGE ON SCHEMA public TO ro_user;

-- Grant SELECT on all existing tables
GRANT SELECT ON ALL TABLES IN SCHEMA public TO ro_user;

-- Ensure SELECT is granted on any future tables created by rw_user
ALTER DEFAULT PRIVILEGES FOR ROLE rw_user IN SCHEMA public
    GRANT SELECT ON TABLES TO ro_user;
