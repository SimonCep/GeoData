# Database tables
```
create table if not exists geodata.`places` (
    `id` int not null auto_increment,
    `population` int not null,
    `rating` double not null,
    `hierarchy` varchar(255),
    primary key (id)
);

create table if not exists geodata.`locations` (
    `id` int not null auto_increment,
    `latitude` double not null,
    `longitude` double not null,
    `altitude` int not null,
    `place_id` int not null,
    primary key (id),
    foreign key (place_id) references `places`(id)
);

create table if not exists geodata.`names` (
    `id` int not null auto_increment,
    `locale` varchar(20) not null,
    `value` varchar(255) not null,
    `place_id` int not null,
    primary key (id),
    foreign key (place_id) references `places`(id)
);

create table if not exists geodata.`tags` (
    `id` int not null auto_increment,
    `key` varchar(255) not null,
    `value` varchar(255) not null,
    `place_id` int not null,
    primary key (id),
    foreign key(place_id) references `places`(id)
);

create table if not exists geodata.`boundaries` (
    `id` int not null auto_increment,
    `geojson` text not null,
    `place_id` int not null,
    primary key (id),
    foreign key(place_id) references `places`(id)
);
```