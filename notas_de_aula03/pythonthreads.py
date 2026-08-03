import threading

def tarefa1():
    for i in range(50):
        print('Thread 1')

def tarefa2():
    for i in range(50):
        print('Thread 2')

t1 = threading.Thread(target=tarefa1)
t2 = threading.Thread(target=tarefa2)

t1.start()
t2.start()