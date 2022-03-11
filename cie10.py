import psycopg2
try:
    #connect to the PostgreSQL server
    conn = psycopg2.connect("host=127.0.0.1 port=5432 dbname=Vehículos user=postgres password=1234 options='-c search_path=entrada_salida,public'")
    #create a cursor object 
    #cursor object is used to interact with the database
    cur = conn.cursor()
    #open the csv file using python standard file I/O
    #copy file into the table
    sql = "COPY \"cie10\" FROM STDIN DELIMITER ',' CSV HEADER"
    cur.copy_expert(sql, open('cie-10.csv', 'r', encoding="utf8"))        
    # close communication with the PostgreSQL database server
    cur.close()
    # commit the changes
    conn.commit()
except (Exception, psycopg2.DatabaseError) as error:
    print(error)
finally:
    if conn is not None:
        conn.close()
