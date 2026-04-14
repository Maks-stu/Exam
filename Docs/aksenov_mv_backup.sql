--
-- PostgreSQL database dump
--

\restrict pV81RatfvbNeL2etPK0ddCcVWDbVyWZdN3LeDsvSoy6h6gey1NONYfiHTGt2dZl

-- Dumped from database version 15.14
-- Dumped by pg_dump version 15.14

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: aksenov_mv; Type: DATABASE; Schema: -; Owner: app
--

CREATE DATABASE aksenov_mv WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE aksenov_mv OWNER TO app;

\unrestrict pV81RatfvbNeL2etPK0ddCcVWDbVyWZdN3LeDsvSoy6h6gey1NONYfiHTGt2dZl
\connect aksenov_mv
\restrict pV81RatfvbNeL2etPK0ddCcVWDbVyWZdN3LeDsvSoy6h6gey1NONYfiHTGt2dZl

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: app; Type: SCHEMA; Schema: -; Owner: app
--

CREATE SCHEMA app;


ALTER SCHEMA app OWNER TO app;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: categories; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.categories (
    id integer NOT NULL,
    name character varying(255)
);


ALTER TABLE app.categories OWNER TO app;

--
-- Name: categories_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.categories_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.categories_id_seq OWNER TO app;

--
-- Name: categories_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.categories_id_seq OWNED BY app.categories.id;


--
-- Name: manufacturers; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.manufacturers (
    id integer NOT NULL,
    name character varying(255)
);


ALTER TABLE app.manufacturers OWNER TO app;

--
-- Name: manufacturers_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.manufacturers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.manufacturers_id_seq OWNER TO app;

--
-- Name: manufacturers_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.manufacturers_id_seq OWNED BY app.manufacturers.id;


--
-- Name: order_items; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.order_items (
    order_id integer NOT NULL,
    product_article character varying(255) NOT NULL,
    quantity integer
);


ALTER TABLE app.order_items OWNER TO app;

--
-- Name: orders; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.orders (
    id integer NOT NULL,
    client_id integer,
    pickup_point_id integer,
    order_date date,
    delivery_date date,
    status character varying(255),
    pickup_code integer
);


ALTER TABLE app.orders OWNER TO app;

--
-- Name: orders_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.orders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.orders_id_seq OWNER TO app;

--
-- Name: orders_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.orders_id_seq OWNED BY app.orders.id;


--
-- Name: pickup_points; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.pickup_points (
    id integer NOT NULL,
    address character varying(255)
);


ALTER TABLE app.pickup_points OWNER TO app;

--
-- Name: pickup_points_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.pickup_points_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.pickup_points_id_seq OWNER TO app;

--
-- Name: pickup_points_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.pickup_points_id_seq OWNED BY app.pickup_points.id;


--
-- Name: products; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.products (
    article character varying(255) NOT NULL,
    name character varying(255),
    unit character varying(50),
    price numeric(10,2),
    category_id integer,
    manufacturer_id integer,
    supplier_id integer,
    discount_percent integer,
    stock_quantity integer,
    description text,
    photo_path character varying(255)
);


ALTER TABLE app.products OWNER TO app;

--
-- Name: roles; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.roles (
    id integer NOT NULL,
    name character varying(255)
);


ALTER TABLE app.roles OWNER TO app;

--
-- Name: roles_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.roles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.roles_id_seq OWNER TO app;

--
-- Name: roles_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.roles_id_seq OWNED BY app.roles.id;


--
-- Name: suppliers; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.suppliers (
    id integer NOT NULL,
    name character varying(255)
);


ALTER TABLE app.suppliers OWNER TO app;

--
-- Name: suppliers_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.suppliers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.suppliers_id_seq OWNER TO app;

--
-- Name: suppliers_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.suppliers_id_seq OWNED BY app.suppliers.id;


--
-- Name: users; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.users (
    id integer NOT NULL,
    login character varying(255),
    password_hash character varying(255),
    full_name character varying(255),
    role_id integer
);


ALTER TABLE app.users OWNER TO app;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.users_id_seq OWNER TO app;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.users_id_seq OWNED BY app.users.id;


--
-- Name: categories id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.categories ALTER COLUMN id SET DEFAULT nextval('app.categories_id_seq'::regclass);


--
-- Name: manufacturers id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.manufacturers ALTER COLUMN id SET DEFAULT nextval('app.manufacturers_id_seq'::regclass);


--
-- Name: orders id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.orders ALTER COLUMN id SET DEFAULT nextval('app.orders_id_seq'::regclass);


--
-- Name: pickup_points id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.pickup_points ALTER COLUMN id SET DEFAULT nextval('app.pickup_points_id_seq'::regclass);


--
-- Name: roles id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.roles ALTER COLUMN id SET DEFAULT nextval('app.roles_id_seq'::regclass);


--
-- Name: suppliers id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.suppliers ALTER COLUMN id SET DEFAULT nextval('app.suppliers_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.users ALTER COLUMN id SET DEFAULT nextval('app.users_id_seq'::regclass);


--
-- Data for Name: categories; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.categories (id, name) FROM stdin;
1	Женская обувь
2	Мужская обувь
\.


--
-- Data for Name: manufacturers; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.manufacturers (id, name) FROM stdin;
1	Рос
2	Marco Tozzi
3	Rieker
4	CROSBY
5	Alessio Nesca
6	Kari
\.


--
-- Data for Name: order_items; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.order_items (order_id, product_article, quantity) FROM stdin;
1	D572U8	10
1	J384T6	10
2	G783F5	1
2	H782T5	1
3	F635R4	2
3	А112Т4	2
4	D329H3	4
4	F572H7	5
5	E482R4	5
5	S213E3	5
6	F635R4	2
6	А112Т4	2
7	G783F5	1
7	H782T5	1
8	D572U8	10
8	J384T6	10
9	D329H3	4
9	F572H7	5
10	G432E4	1
10	B320R5	5
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.orders (id, client_id, pickup_point_id, order_date, delivery_date, status, pickup_code) FROM stdin;
1	2	3	2025-02-28	2025-04-26	Завершен	907
2	1	11	2022-09-28	2025-04-21	Завершен	902
3	4	2	2025-03-17	2025-04-24	Завершен	905
4	3	19	2025-03-31	2025-04-27	Новый	908
5	4	19	2025-04-03	2025-04-29	Новый	910
6	4	1	2025-02-27	2025-04-20	Завершен	901
7	1	15	2025-03-01	2025-04-25	Завершен	906
8	2	2	2025-03-21	2025-04-22	Завершен	903
9	3	11	2025-02-20	2025-04-23	Завершен	904
10	4	5	2025-04-02	2025-04-28	Новый	909
\.


--
-- Data for Name: pickup_points; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.pickup_points (id, address) FROM stdin;
1	420151, г. Лесной, ул. Вишневая, 32
2	125061, г. Лесной, ул. Подгорная, 8
3	630370, г. Лесной, ул. Шоссейная, 24
4	400562, г. Лесной, ул. Зеленая, 32
5	614510, г. Лесной, ул. Маяковского, 47
6	410542, г. Лесной, ул. Светлая, 46
7	620839, г. Лесной, ул. Цветочная, 8
8	443890, г. Лесной, ул. Коммунистическая, 1
9	603379, г. Лесной, ул. Спортивная, 46
10	603721, г. Лесной, ул. Гоголя, 41
11	410172, г. Лесной, ул. Северная, 13
12	614611, г. Лесной, ул. Молодежная, 50
13	454311, г.Лесной, ул. Новая, 19
14	660007, г.Лесной, ул. Октябрьская, 19
15	603036, г. Лесной, ул. Садовая, 4
16	394060, г.Лесной, ул. Фрунзе, 43
17	410661, г. Лесной, ул. Школьная, 50
18	625590, г. Лесной, ул. Коммунистическая, 20
19	625683, г. Лесной, ул. 8 Марта
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.products (article, name, unit, price, category_id, manufacturer_id, supplier_id, discount_percent, stock_quantity, description, photo_path) FROM stdin;
D329H3	Полуботинки	шт.	1890.00	1	5	1	4	4	Полуботинки Alessio Nesca женские 3-30797-47, размер 37, цвет: бордовый	8.jpg
S326R5	Тапочки	шт.	9900.00	2	4	1	17	15	Мужские кожаные тапочки Профиль С.Дали	\N
K345R4	Полуботинки	шт.	2100.00	2	4	1	2	3	407700/01-02 Полуботинки мужские CROSBY	\N
S634B5	Кеды	шт.	5500.00	2	4	1	3	0	Кеды Caprice мужские демисезонные, размер 42, цвет черный	\N
S213E3	Полуботинки	шт.	2156.00	2	4	1	3	6	407700/01-01 Полуботинки мужские CROSBY	\N
D268G5	Туфли	шт.	4399.00	1	3	1	3	12	Туфли Rieker женские демисезонные, размер 36, цвет коричневый	\N
F427R5	Ботинки	шт.	11800.00	1	3	1	15	11	Ботинки на молнии с декоративной пряжкой FRAU	\N
O754F4	Туфли	шт.	5400.00	1	3	1	4	18	Туфли женские демисезонные Rieker артикул 55073-68/37	\N
M542T5	Кроссовки	шт.	2800.00	2	3	1	18	3	Кроссовки мужские TOFA	\N
B431R5	Ботинки	шт.	2700.00	2	3	1	2	5	Мужские кожаные ботинки/мужские ботинки	\N
J384T6	Ботинки	шт.	3800.00	2	3	1	2	16	B3430/14 Полуботинки мужские Rieker	5.jpg
F635R4	Ботинки	шт.	3244.00	1	2	1	2	13	Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый	2.jpg
D572U8	Кроссовки	шт.	4100.00	2	1	1	3	6	129615-4 Кроссовки мужские	6.jpg
L754R4	Полуботинки	шт.	1700.00	1	6	2	2	7	Полуботинки kari женские WB2020SS-26, размер 38, цвет: черный	\N
D364R4	Туфли	шт.	12400.00	1	6	2	16	5	Туфли Luiza Belly женские Kate-lazo черные из натуральной замши	\N
G531F4	Ботинки	шт.	6600.00	1	6	2	12	9	Ботинки женские зимние ROMER арт. 893167-01 Черный	\N
E482R4	Полуботинки	шт.	1800.00	1	6	2	2	14	Полуботинки kari женские MYZ20S-149, размер 41, цвет: черный	\N
G432E4	Туфли	шт.	2800.00	1	6	2	3	15	Туфли kari женские TR-YR-413017, размер 37, цвет: черный	10.jpg
А112Т4	Ботинки	шт.	4990.00	1	6	2	3	6	Женские Ботинки демисезонные kari	1.jpg
J542F5	Тапочки	шт.	500.00	2	6	2	13	0	Тапочки мужские Арт.70701-55-67син р.41	\N
H782T5	Туфли	шт.	4499.00	2	6	2	4	5	Туфли kari мужские классика MYZ21AW-450A, размер 43, цвет: черный	3.jpg
C436G5	Ботинки	шт.	10200.00	1	5	2	15	9	Ботинки женские, ARGO, размер 40	\N
T324F5	Сапоги	шт.	4699.00	1	4	2	2	5	Сапоги замша Цвет: синий	\N
N457T5	Полуботинки	шт.	4600.00	1	4	2	3	13	Полуботинки Ботинки черные зимние, мех	\N
P764G4	Туфли	шт.	6800.00	1	4	2	15	15	Туфли женские, ARGO, размер 38	\N
B320R5	Туфли	шт.	4300.00	1	3	2	2	6	Туфли Rieker женские демисезонные, размер 41, цвет коричневый	9.jpg
K358H6	Тапочки	шт.	599.00	2	3	2	20	2	Тапочки мужские син р.41	\N
F572H7	Туфли	шт.	2700.00	1	2	2	2	14	Туфли Marco Tozzi женские летние, размер 39, цвет черный	7.jpg
G783F5	Ботинки	шт.	5900.00	2	1	2	2	8	Мужские ботинки Рос-Обувь кожаные с натуральным мехом	4.jpg
H535R5	Ботинки	шт.	2300.00	1	3	1	2	7	Женские Ботинки демисезонные	picture.png
\.


--
-- Data for Name: roles; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.roles (id, name) FROM stdin;
2	Менеджер
3	Администратор
1	Авторизированный Клиент
\.


--
-- Data for Name: suppliers; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.suppliers (id, name) FROM stdin;
1	Обувь для вас
2	Kari
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.users (id, login, password_hash, full_name, role_id) FROM stdin;
1	94d5ous@gmail.com	uzWC67	Никифорова Весения Николаевна	3
2	uth4iz@mail.com	2L6KZG	Сазонов Руслан Германович	3
3	yzls62@outlook.com	JlFRCZ	Одинцов Серафим Артёмович	3
4	1diph5e@tutanota.com	8ntwUp	Степанов Михаил Артёмович	2
5	tjde7c@yahoo.com	YOyhfR	Ворсин Петр Евгеньевич	2
6	wpmrc3do@tutanota.com	RSbvHv	Старикова Елена Павловна	2
7	5d4zbu@tutanota.com	rwVDh9	Михайлюк Анна Вячеславовна	1
8	ptec8ym@yahoo.com	LdNyos	Ситдикова Елена Анатольевна	1
9	1qz4kw@mail.com	gynQMT	Ворсин Петр Евгеньевич	1
10	4np6se@mail.com	AtnDjr	Старикова Елена Павловна	1
\.


--
-- Name: categories_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.categories_id_seq', 1, false);


--
-- Name: manufacturers_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.manufacturers_id_seq', 1, false);


--
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.orders_id_seq', 1, false);


--
-- Name: pickup_points_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.pickup_points_id_seq', 1, false);


--
-- Name: roles_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.roles_id_seq', 1, false);


--
-- Name: suppliers_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.suppliers_id_seq', 1, false);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.users_id_seq', 1, false);


--
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);


--
-- Name: manufacturers manufacturers_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.manufacturers
    ADD CONSTRAINT manufacturers_pkey PRIMARY KEY (id);


--
-- Name: order_items order_items_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.order_items
    ADD CONSTRAINT order_items_pkey PRIMARY KEY (order_id, product_article);


--
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- Name: pickup_points pickup_points_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.pickup_points
    ADD CONSTRAINT pickup_points_pkey PRIMARY KEY (id);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (article);


--
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);


--
-- Name: suppliers suppliers_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.suppliers
    ADD CONSTRAINT suppliers_pkey PRIMARY KEY (id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: order_items order_items_order_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.order_items
    ADD CONSTRAINT order_items_order_id_fkey FOREIGN KEY (order_id) REFERENCES app.orders(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: order_items order_items_product_article_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.order_items
    ADD CONSTRAINT order_items_product_article_fkey FOREIGN KEY (product_article) REFERENCES app.products(article) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: orders orders_client_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.orders
    ADD CONSTRAINT orders_client_id_fkey FOREIGN KEY (client_id) REFERENCES app.users(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: orders orders_pickup_point_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.orders
    ADD CONSTRAINT orders_pickup_point_id_fkey FOREIGN KEY (pickup_point_id) REFERENCES app.pickup_points(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: products products_category_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_category_id_fkey FOREIGN KEY (category_id) REFERENCES app.categories(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: products products_manufacturer_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_manufacturer_id_fkey FOREIGN KEY (manufacturer_id) REFERENCES app.manufacturers(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: products products_supplier_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_supplier_id_fkey FOREIGN KEY (supplier_id) REFERENCES app.suppliers(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: users users_role_id_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.users
    ADD CONSTRAINT users_role_id_fkey FOREIGN KEY (role_id) REFERENCES app.roles(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict pV81RatfvbNeL2etPK0ddCcVWDbVyWZdN3LeDsvSoy6h6gey1NONYfiHTGt2dZl

