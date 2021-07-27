# IBM-Kubernetes-OpenShift-Application-.Net ☁📱

Esta guía está enfocada en el despliegue de una aplicación ASP.NET Core en Kubernetes junto con una base de datos SQL Server. 

## Índice  📰
1. [Pre-Requisitos](#Pre-Requisitos-pencil)
2. [Descripción y funcionamiento de la aplicación](#Descripción-y-funcionamiento-de-la-aplicación-mag_right)

### Sección 1 - Kubernetes
3. [Clonar Repositorio](#Clonar-Repositorio-pushpin-file_folder)
4. [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud)
5. [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)
6. [Crear imagen de la aplicación](#Crear-imagen-de-la-aplicación-calling)
7. [Subir imagen de la aplicación a IBM Cloud Container Registry](#Subir-imagen-de-la-aplicación-a-IBM-Cloud-Container-Registry-cloud-books)
8. [Desplegar imagen de aplicación en Kubernetes](#Desplegar-imagen-de-aplicación-en-Kubernetes-cloud-rocket)
9. [Prueba de Funcionamiento en Kubernetes](#Prueba-de-Funcionamiento-en-Kubernetes-trophy)
10. [Visualizar tablas de base de datos en SSMS](#Visualizar-tablas-de-base-de-datos-en-SSMS-computer)

### Sección 2 - OpenShift
11. [Desplegar imagen de SQL Server en OpenShift](#Desplegar-imagen-de-SQL-Server-en-OpenShift-outbox_tray-cloud)
12. [Desplegar aplicación en OpenShift](#Desplegar-aplicación-en-OpenShift-cloud-rocket)
13. [Prueba de Funcionamiento en OpenShift](#Prueba-de-Funcionamiento-en-OpenShift-trophy)
<br />

## Pre-requisitos :pencil:
* Tener instalado *Git* en su computador para clonar el respositorio.
* Tener instalada la CLI de *Docker*.
* Tener instalado *Docker Desktop* para verificar la creación de su imagen.
* Tener instalada la CLI de *IBM Cloud*.
* Contar con una cuenta en <a href="https://cloud.ibm.com/"> IBM Cloud </a>.
* Contar con un clúster en Kubernetes.
* Contar con un clúster en OpenShift.
* Tener instalado <a href="https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15"> SQL Server Management Studio </a>.
* Tener instalado Visual Studio 2019.
<br />

## Descripción y funcionamiento de la aplicación :mag_right:
<br />

## Sección 1 - Kubernetes 💡
## Clonar Repositorio :pushpin: :file_folder:
La aplicación utilizada en esta guía la puede encontrar en este repositorio. Para clonar el repositorio en su computador, realice los siguientes pasos:

1. En su computador cree una carpeta a la que pueda acceder con facilidad y asígnele un nombre relacionado con la aplicación.
2. Abra una ventana de *Windows PowerShell* y vaya hasta la carpeta que creó en el ítem 1 con el comando ```cd```.
3. Una vez se encuentre dentro de la carpeta creada coloque el siguiente comando para clonar el repositorio:
```
git clone https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net.git
```
4. Acceda a la carpeta **"IBM-Kubernetes-Applicacion-.Net"** creada al clonar el repositorio y verifique que se encuentran descargados los archivos de la aplicación que se muestran en este repositorio.
<br />

## Desplegar imagen de SQL Server en Kubernetes :outbox_tray: :cloud:
Para realizar el despliegue de la imagen de SQL Server en Kubernetes, se utiliza *Persistent Volume Claims (PVC)*, que consiste en realizar una solicitud de almacenamiento a Kubernetes a un *Persistent Volume (PV)*. Este almacenamiento se puede solicitar en ```Mi (MB)``` o ```Gi (GB)```. 

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
* Nombre: ```mssql-pvc```.
* Modo de acceso:  ```ReadWriteOnce``` para permitir que el *Persisten Volume* pueda ser leído y escrito por un solo nodo trabajador a la vez.
* Cantidad de almacenamiento, en este caso es de ```1 MB```.
<br />

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
* Nombre del despliegue: ```mssql-deployment```
* La imagen de SQL Server que se utilizará: ```mcr.microsoft.com/mssql/server:2019-latest```.
* El puerto de escucha TCP, por defecto es el ```1433```.
* Variables de entorno (*env*): estas variables deben coincidir con la cadena de conexión que se establece en la aplicación (ver [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)). Es importante reemplazar ```<password>``` con la contraseña establecida. En los archivos del repositorio se indicó un valor para la  contraseña, pero si desea puede modificarla.
* La ruta de montaje: se define la ruta dentro del contenedor donde se montará el *Persistent Volume*. Para este caso: ```./data:/var/opt/mssql/data```.
* El nombre del *Persisten Volume Claim* para realizar la solicitud: ```mssql-pvc```.
 <br />
  
3. ```sql-service.yaml``` 
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
Este archivo es de tipo *service*. Allí se establece la respectiva configuración indicando:
* Nombre del servicio: ```mssql-service```.
* El puerto ```1433``` se abre en el servicio y está configurado el puerto de destino ```1433``` para el contenedor del servidor SQL.
<br />

Una vez configurados y explicados los archivos necesarios, se deben emplear los siguientes comandos para realizar el despliegue de la imagen de SQL Server en el clúster de Kubernetes. Para ello, siga los pasos que se muestran a continuación:

1. En *Windows PowerShell* y con el comando ```cd``` vaya a los archivos de la carpeta **SQL Server - Despliegue en Kubernetes** (recuerde que está la encuentra luego de clonar el repositorio en su máquina local) y coloque:
```
ibmcloud login --sso
```
<br />

2. Seleccione la cuenta en donde se encuentra su clúster de Kubernetes.
<br />

3. Una vez ha iniciado sesión, configure el grupo de recursos y la región que está utilizando su clúster de Kubernetes. Para ello utilice el siguiente comando:
```
ibmcloud target -r <REGION> -g <GRUPO_RECURSOS>
```
>**Nota**: Reemplace \<REGION> y <GRUPO_RECURSOS> con su información.
<br />

4. Obtenga la lista de clústers de Kubernetes que hay en la cuenta establecida en el ítem 2:
```
ibmcloud cs clusters
```
<br />

5. Verifique el nombre de clúster en el que va a desplegar la imagen y habilite el comando ```kubectl``` de la siguiente manera
```
ibmcloud ks cluster config --cluster <cluster_name>
```
<br />

6. Para crear un *Persistent Volume Claim (PVC)* utilice el comando:
```
kubectl apply -f my-pvc.yaml
```
<br />

Si desea observar el *Persistent Volume Claim (PVC)* que acaba de crear, utilice el comando:
```
kubectl get PersistentVolumeClaim
```
<br />

7. Posteriormente, se debe crear un despliegue de SQL Server en Kubernetes, que cuente con un *Persistent Volume (PV)* para almacenar los datos de la aplicación. Para ello coloque el comando:
```
kubectl apply -f sql-dep.yaml
```
<br />

9. Para finalizar, se debe crear un servicio para exponer el pod de SQL Server a otros pods en Kubernetes. Para ello coloque:
```
kubectl apply -f sql-service.yaml
```

Verifique en Kubernetes que aparezca:
* Persistent Volume Claims: ```mssql-pvc```
* Deployments: ```mssql-deployment```
* Services: ```mssql-service```

<br />

## Configurar cadena de conexión en aplicación :hammer:
Para realizar la respectiva conexión entre la aplicación y SQL Server en Kubernetes, se debe configurar la cadena de conexión teniendo en cuenta los parámetros establecidos al momento de desplegar la imagen de SQL Server. Para ello en el archivo ```appsettings.json``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core/InAndOut```, establezca los siguientes parámetros:

```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=mssql-service,1433;Initial Catalog=MyNetDB;Persist Security Info=true;User ID=SA;Password=<password>; MultipleActiveResultSets=true"
  },
 ```
 Tenga en cuenta:
 * Data Source = ```mssql-service,1433```, teniendo en cuenta el nombre del servicio expuesto para el Pod de SQL Server y el puerto de destino del contenedor.
 * Initial Catalog = ```MyNetDB```, corresponde al nombre de la base de datos en donde se va a almacenar la información. Si desea puede asignarle otro nombre.
 * Persist Security Info = ```true```
 * User ID = ```SA```, es el usuario. Por defecto deje este valor. 
 * Password = ```<password>```, en los archivos del repositorio se indicó un valor para la contraseña, pero si desea puede modificarla. Debe tener en cuenta que si modificó la contraseña en el item 2 del paso: [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud), debe colocar en la cadena de conexión de la aplicación esa misma contraseña. 
 * MultipleActiveResultSets = ```true```

### NOTA: 
> ***SOLO*** en caso de realizar alguna modificación a la cadena de conexión (por ejemplo: cambios en el nombre de la base de datos o la contraseña) debe realizar nuevamente las migraciones (son el proceso mediante el cual se mueven datos hacia o desde SQL Server). Para ello, realice lo siguiente:

* Elimine la carpeta ```Migrations``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core/InAndOut/```. 
* Abra el proyecto en Visual Studio 2019, de click en la ```Consola del Administrador de paquetes``` y coloque el siguiente comando:
```
add-migration <migration_name>
```
> Reemplace \<migration_name> con un nombre que le permita identificar la migración, por ejemplo: MigracionFinal.

* Teniendo en cuenta que realizó cambios en la aplicación, debe volver a publicarla. Para ello, elimine la carpeta ```Release``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core/InAndOut/bin/```. Posteriormente en *Windows PowerShell* y asegurandose de estar dentro de la carpeta ```InAndOut``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core/``` coloque el siguiente comando:
```
dotnet publish -c Release
``` 
 
<br />

## Crear imagen de la aplicación :calling:
Al clonar este repositorio puede encontrar dentro de los archivos el *Dockerfile* utilizado para crear la imagen de la aplicación. Realice los siguientes pasos:
1. En la ventaja de *Windows PowerShell* y asegurándose que se encuentra dentro de la carpeta que contiene los archivos de la aplicación (```InAndOut```) y el Dockerfile, coloque el siguiente comando para crear la imagen de su aplicación:
```
docker build -t <nombre_imagen:tag> .
```
> Nota: Reemplace <nombre_imagen:tag> con un nombre y una etiqueta que le permita identificar su imagen.
<br />

2. Una vez finalice el proceso, verifique en *Docker Desktop* que la imagen que acaba de crear aparece en la lista de imágenes.
<br />


## Subir imagen de la aplicación a IBM Cloud Container Registry :cloud: :books:
Para subir la imagen creada a *IBM Cloud Container Registry* realice lo siguiente:
1. En la ventana de *Windows PowerShell* y sin salir en ningún momento de la carpeta que contiene los archivos (```InAndOut```), inicie sesión en su cuenta de *IBM Cloud* con el siguiente comando:
```
ibmcloud login --sso
```
<br />

2. Seleccione la cuenta en donde se encuentra su clúster de Kubernetes.
<br />

3. Una vez ha iniciado sesión, configure el grupo de recursos y la región que está utilizando su clúster de Kubernetes. Para ello utilice el siguiente comando:
```
ibmcloud target -r <REGION> -g <GRUPO_RECURSOS>
```
>**Nota**: Reemplace \<REGION> y <GRUPO_RECURSOS> con su información.
<br />


4. Registre el daemon de Docker local en *IBM Cloud Container Registry* con el comando:
```
ibmcloud cr login
```
<br />

5. Cree un espacio de nombres (*namespace*) dentro de *IBM Cloud Container Registry* para su imagen. Para ello ejecute el siguiente comando:
```
ibmcloud cr namespace-add <namespace>
```
>**Nota**: Reemplace \<namespace> con un nombre fácil de recordar y que esté relacionado con la imagen de la aplicación. 
<br />

6. Elija un repositorio y una etiqueta con la que pueda identificar su imagen. En este caso, debe colocar la información de la imagen que creó en *Docker* y el espacio de nombres (*namespace*) creado en el ítem anterior. Coloque el siguiente comando:
```
docker tag <nombre_imagen:tag> us.icr.io/<namespace>/<nombre_imagen:tag>
```
>**Nota**: En el nombre de dominio **us.icr.io**, debe tener en cuenta colocar el dato correcto en base a la región en donde se encuentra su clúster y grupo de recursos. Para mayor información puede consultar <a href="https://cloud.ibm.com/docs/Registry?topic=Registry-registry_overview#registry_regions"> regiones </a>.
<br />

7. Envíe la imagen a *IBM Cloud Container Registry* mediante el comando:
```
docker push us.icr.io/<namespace>/<nombre_imagen:tag>
```
<br />

8. Verifique en *IBM Cloud Container Registry* que aparece el espacio de nombres (namespace), el repositorio y la imagen. Tenga en cuenta los nombres que asignó en cada paso.
<br />



## Desplegar imagen de aplicación en Kubernetes :cloud: :rocket:
Para desplegar la imagen de la aplicación en Kubernetes, realice lo siguiente:
1. En la ventana de *Windows PowerShell* en la que ha trabajado, coloque el siguiente comando para ver la lista de clústers de Kubernetes que hay en su cuenta:
```
ibmcloud cs clusters
```
<br />

2. Verifique el nombre de clúster en el que va a desplegar la imagen y habilite el comando kubectl de la siguiente manera:
```
ibmcloud ks cluster config --cluster <cluster_name>
```
<br />

3. Cree el servicio de despliegue en Kubernetes, para esto, ejecute los comandos que se muestran a continuación (recuerde cambiar \<deployment> con un nombre para su servicio de despliegue):  
```
kubectl create deployment <deployment> --image=us.icr.io/<namespace>/<nombre_imagen:tag>
```
<br />
  
4. A continuación, debe exponer su servicio en Kubernetes, para ello realice lo siguiente.
>**NOTA 1**: Si esta trabajando con infraestructura clásica ejecute el siguiente comando:

```
kubectl expose deployment/<deployment> --type=NodePort --port=8080
```

>**NOTA 2**: Si esta trabajando con VPC (Load Balancer) ejecute el siguiente comando:
```
kubectl expose deployment/<deployment> --type=LoadBalancer --name=<service>  --port=8080 --target-port=8080
```
En la etiqueta **\<service>** indique un nombre para su servicio. Recuerde colocar el valor del puerto en base a lo establecido en el Dockerfile de la aplicación.

<br />


5. Por último verifique que el deployment y el service creados aparecen de forma exitosa en el panel de control de su clúster.
<br />



## Prueba de Funcionamiento en Kubernetes :trophy:
Para verificar el correcto funcionamiento de su aplicación en Kubernetes realice lo siguiente:

1. Si trabaja con infraestructura clásica su aplicación funcionará si coloca en el navegador **IP_Publica:port**. Para obtener la IP Pública coloque el comando:
```
ibmcloud ks workers --cluster <ID_Cluster>
```

Para obtener el puerto use el comando:
```
kubectl get service <deployment>
```
<br />

2. Si trabaja con VPC (Load Balancer), diríjase a la pestaña Service/Services dentro del panel de control de Kubernetes, visualice el servicio creado y de click en el external endpoint.  
<br />

3. Visualice las diferentes ventanas de la aplicación y realice pruebas con datos en las secciones de ```Transferencias```, ```Gastos``` y ```Tipos de Gastos```.

<br />


## Visualizar tablas de base de datos en SSMS :computer:
Para visualizar las tablas de la base de datos de forma local en *SQL Server Management Studio* realice lo siguiente:
1. *En Windows PowerShell* visualice el Pod de SQL Server mediante el comando:
```
kubectl get pods
```
Allí, observará la lista de Pods que se ejecutan en su clúster de Kubernetes. Visualice el del SQL Server y copie el nombre para usarlo más adelante.
<br />

2. Reenvíe la conexión del puerto del Pod a un puerto local que no esté usando en su máquina, para ello utilice el comando:
```
kubectl port-forward pod/<name_pod> <puerto_local>:1433 
```
> **NOTA**: Reemplace <name_pod> con el nombre del Pod de SQL Server en Kuberntes y <puerto_local> con un puerto que no esté usando en su máquina local, por ejemplo: 15789. Como resultado, una vez ejecute el comando anterior obtendrá: *127.0.0.1:<puerto_local>*
<br />

3. Para visualizar las tablas de datos y la información registrada en las pruebas de funcionamiento, abra *SQL Server Management Studio* y complete los campos con la siguiente información:
* Service Type: ```Database Engine```.
* Server name: ```127.0.0.1,<puerto_local>```.
* Authentication: ```SQL Server Authentication```.
* Login:```SA```.
* Password: ```<password>```.
> **NOTA**: Recuerde reemplazar <password> con la contraseña. Si dejó la que se estableció en los archivos de la aplicación, coloque la misma aquí, de lo contrario coloque la contraseña que asignó en los pasos [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud)
y [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)


<br />


## Sección 2 💡
## Desplegar imagen de SQL Server en OpenShift :outbox_tray: :cloud:
<br />

## Desplegar aplicación en OpenShift :cloud: :rocket:
<br />

## Prueba de Funcionamiento en OpenShift :trophy:
<br />


## Autores ✒
Equipo IBM Cloud Tech Sales Colombia.
