# IBM-Kubernetes-OpenShift-Application-.Net ☁📱

Esta guía está enfocada en el despliegue de una aplicación ASP.NET Core en Kubernetes junto con una base de datos SQL Server. 

## Índice  📰
1. [Pre-Requisitos](#Pre-Requisitos-pencil)

### Sección 1 - Kubernetes.
2. [Paso 1. Clonar Repositorio](#Paso-1)
3. [Paso 2. Desplegar imagen de SQL Server en Kubernetes](#Paso-2)
4. [Paso 3. Configurar cadena de conexión en aplicación ASP.NET Core](#Paso-3)
5. [Paso 4. Crear imagen de la aplicación ASP.NET Core](#Paso-4)
6. [Paso 5. Desplegar imagen de aplicación en Kubernetes](#Paso-5)
7. [Paso 6. Prueba de Funcionamiento en Kubernetes](#Paso-6)
8. [Paso 7. Visualizar tablas de base de datos en SSMS](#Paso-7)

### Sección 2 - OpenShift.
9. [Paso 8. Desplegar imagen de SQL Server en OpenShift](#Paso-8)
10. [Paso 9. Desplegar aplicación en OpenShift](#Paso-9)
11. [Paso 10. Prueba de Funcionamiento en OpenShift](#Paso-10)

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

## Sección 1 - Kubernetes. 💡
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

## Paso 2
### Desplegar imagen de SQL Server en Kubernetes📤☁
Para realizar el despliegue de la imagen de SQL Server en Kubernetes, se utiliza *Persistent Volume Claims (PVC)*, que consiste en realizar una solicitud de almacenamiento a Kubernetes a un *Persistent Volume (PV)*. Este almacenamiento se puede solicitar en Mi(MB) o Gi(GB). 

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
Este archivo es de tipo *PersistentVolumeClaim*. Allí se establece la respectiva configuración indicando: 
* Nombre.
* Modo de acceso:  *ReadWriteOnce* para permitir que el *Persisten Volume* pueda ser leído y escrito por un solo nodo trabajador a la vez.
* Cantidad de almacenamiento, en este caso es de 1 MB.

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
Este archivo es de tipo *deployment*. Allí se establece la respectiva configuración indicando: 
* Nombre del despliegue: mssql-deployment
* La imagen de SQL Server que se utilizará: *mcr.microsoft.com/mssql/server:2019-latest*.
* El puerto de escucha TCP, por defecto es el 1433.
* Variables de entorno (*env*): estas variables deben coincidir con la cadena de conexión que se establece en la aplicación ([Paso 3. Configurar cadena de conexión en aplicación ASP.NET Core](#Paso-3)). Es importante reemplazar *\<password>* con la contraseña establecida. En los archivos del repositorio se indicó un valor para la  contraseña, pero si desea puede modificarla.
* La ruta de montaje: se define la ruta dentro del contenedor donde se montará el *Persistent Volume*. Para este caso: *./data:/var/opt/mssql/data*.
* El nombre del *Persisten Volume Claim* para realizar la solicitud.
  
  


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

## Paso 10
### Prueba de Funcionamiento en OpenShift 🚀


## Autores ✒
Equipo IBM Cloud Tech Sales Colombia.
