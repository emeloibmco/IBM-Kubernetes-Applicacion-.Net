# IBM-Kubernetes-OpenShift-Application-.Net ☁📱

Esta guía está enfocada en el despliegue de una aplicación ASP.NET Core en Kubernetes junto con una base de datos SQL Server. 

## Índice  📰
1. [Pre-Requisitos](#Pre-Requisitos-pencil)
2. [Paso 1. Clonar Repositorio](#Paso-1)

### Sección 1.
4. [Paso 2. Desplegar imagen de SQL Server en Kubernetes](#Paso-2)
5. [Paso 3. Configurar cadena de conexión en aplicación ASP.NET Core](#Paso-3)
6. [Paso 4. Crear imagen de la aplicación ASP.NET Core](#Paso-4)
7. [Paso 5. Desplegar imagen de aplicación en Kubernetes](#Paso-5)
8. [Paso 6. Prueba de Funcionamiento en Kubernetes](#Paso-6)
9. [Paso 7. Visualizar tablas de base de datos en SSMS](#Paso-7)

### Sección 2.
11. [Paso 8. Desplegar imagen de SQL Server en OpenShift](#Paso-8)
12. [Paso 9. Desplegar aplicación en OpenShift](#Paso-9)
13. [Paso 10. Prueba de Funcionamiento en OpenShift](#Paso-10)

## Pre-requisitos :pencil:
* Tener instalado *Git* en su computador para clonar el respositorio.
* Tener instalada la CLI de *Docker*.
* Tener instalado *Docker Desktop* para verificar la creación de su imagen.
* Tener instalada la CLI de *IBM Cloud*.
* Contar con una cuenta en <a href="https://cloud.ibm.com/"> IBM Cloud </a>.
* Contar con un clúster en Kubernetes.
* Contar con un clúster en OpenShift.
* Tener instalado <a href="https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15"> SQL Server Management Studio </a>.
* Tener instalado Visual Studio 2019 o Visual Studio Code.

## Paso 1
### Clonar Repositorio 📍📁
La aplicación utilizada en esta guía la puede encontrar en este repositorio. Para clonar el repositorio en su computador, realice los siguientes pasos:

1. En su computador cree una carpeta a la que pueda acceder con facilidad y asígnele un nombre relacionado con la aplicación.
2. Abra una ventana de *Windows PowerShell* y vaya hasta la carpeta que creó en el ítem 1 con el comando ```cd```.
3. Una vez se encuentre dentro de la carpeta creada coloque el siguiente comando para clonar el repositorio:
```
git clone https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net.git
```
4. Acceda a la carpeta **"IBM-Kubernetes-Applicacion-.Net"** creada al clonar el repositorio y verifique que se encuentran descargados los archivos de la aplicación que se muestran en este repositorio.

## Sección 1. 💡

## Paso 2
### Desplegar imagen de SQL Server en Kubernetes📤☁
Para realizar el despliegue de la imagen de SQL Server en Kubernetes, se utiliza *Persistent Volum Claims (PVC)*, que consiste en realizar una solicitud de almacenamiento a Kubernetes a un *Persistent Volum (PV)*. Este almacenamiento se puede solicitar en Mi(MB) o Gi(GB). 

Para este caso, se cuenta con 3 archivos de extenxión ```.yaml```, que puede encontrar en la carpeta **SQL Server - Despliegue en Kubernetes**. La explicación de cada archivo se presenta a continuación:

1. ```my-pvc.yaml```
```
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: mssql-pvc
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 1Mi
```
2. ```sql-dep.yaml```
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mssql-deployment
spec:
  replicas: 1
  selector:
     matchLabels:
       app: mssql
  template:
    metadata:
      labels:
        app: mssql
    spec:
      containers:
      - name: mssql
        image: mcr.microsoft.com/mssql/server:2019-latest
        ports:
        - containerPort: 1433
        env:
        - name: MSSQL_PID
          value: "Developer"
        - name: ACCEPT_EULA
          value: "Y"
        - name: SA_PASSWORD
          value: "<password>"
        volumeMounts:
        - name: mssqldb
          mountPath: ./data:/var/opt/mssql/data
      volumes:
      - name: mssqldb
        persistentVolumeClaim:
          claimName: mssql-pvc
```


4. ```sql-service.yaml``` 
```
apiVersion: v1
kind: Service
metadata:
  name: mssql-service
spec:
  selector:
    app: mssql
  type: NodePort  
  ports:
    - protocol: TCP
      port: 1433
      targetPort: 1433
```

## Paso 3
### Configurar cadena de conexión en aplicación 🛠

## Paso 4
### Crear imagen de la aplicación 📲

## Paso 5
### Desplegar imagen de aplicación en Kubernetes 📤☁

## Paso 6
### Prueba de Funcionamiento en Kubernetes 🚀

## Paso 7
### Visualizar tablas de base de datos en SSMS 📇💻


## Sección 2. 💡
## Paso 8
### Desplegar imagen de SQL Server en OpenShift 📤☁

## Paso 9
### Desplegar aplicación en OpenShift 📤☁

## Paso 19
### Prueba de Funcionamiento en OpenShift 🚀


## Autores ✒
Equipo IBM Cloud Tech Sales Colombia.
