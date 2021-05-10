import os
import sys
from howlongtobeatpy import HowLongToBeat

# Función para escribir un archivo con los resultados obtenidos, será leído desde C#    os.getcwd() +
def write_response(res):
    fix_path = str(sys.argv[2])
    if os.path.isfile(fix_path):  # Si el archivo existe...
        os.remove(fix_path)       # Borra el archivo actual
    file = open(fix_path, 'w')    # Abre el archivo (si no existe lo crea)
    file.write(res)               # Escribe la respuesta obtenida
    file.close()

# Función para dar formato a los números antes de ser escritos en un archivo
def format_result(time_to_beat):
    half_index = time_to_beat.find('½')               # Retorna el índice donde se encuentra el caracter '½'
    if half_index != -1:                              # Si el caracter '½' existe en el string...
        return time_to_beat[0:half_index] + '.30'     # retorna el número antes del '½' y lo sustituye por '.30'
    return time_to_beat                               # De lo contrario retorna el número de horas (sin 'h.')

# Función principal: lee los juegos escritos en .txt, obtiene el HLTB para cada uno y escribe su resultado en un .txt
def how_long_to_beat():
    try:
        currentPath = os.getcwd()
        file_content = ""                                       # string para dar formato al nuevo archivo txt
        file = open(str(sys.argv[1]), 'r') #os.getcwd() + '\\PySharp\\games.txt", 'r')  # lee el archivo con el nombre de los juegos
        for line in file:                                       # para cada uno de los nombres de juegos...
            results = HowLongToBeat(0).search(line[:-1])        # se le resta 1 a line para no tomar en cuenta '\n'
            result = max(results, key=lambda element: element.similarity).gameplay_main
            if result!=-1:
                n_result = format_result(result)                   # Da formato al valor obtenido, intercambia '½' por '.30'
            else:
                n_result="n/a"
            file_content = file_content + line[:-1] + ";" + n_result + '\n' # Formato de escritura: 'nombreJuego:horas'
        write_response(file_content)                           # Llamada a la función que crea el nuevo archivo
    except ValueError as e:
        print(e)

how_long_to_beat()