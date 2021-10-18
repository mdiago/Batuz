# Batuz

Nuevo modelo para la tributación de los rendimientos empresariales implementado por las autoridades fiscales Vascas.

## Aplicación de la tecnología blockchain en el ámbito fiscal

En el año 2022, comienza la aplicación del proyecto Batuz. La administración foral vasca ha puesto en marcha este proyecto para luchar contra el fraude fiscal, y facilitar la gestión de la imposición sobre los rendimientos empresariales (IS e IRPF), aplicando el sistema de facturación en lugar del sistema de autoliquidación. En el sistema de facturación, es la administración tributaria, la que de manera proactiva, calcula y liquida el impuesto al contribuyente, mediante los datos recopilados por la misma con anterioridad. Esto significa que una porción importante de la información financiera de las empresas va a tener que comunicarse a las autoridades fiscales, en unos términos y plazos similares a los del actual SII de la AEAT.

El papel del software, los certificados electrónicos y los dispositivos de facturación, cobran especial relevancia en este proyecto. El proyecto Batuz se compone de:

* Los **libros contables**: Ventas e ingresos, Compras y gastos, Bienes de inversión. La información de los cuales debe transmitirse a las autoridades fiscales.
* **TicketBAI**: Estándar que aplica la tecnología blockchain para la emisión de facturas o justificantes de ventas.

## TicketBAI

Es la definición del sistema informático garante de la **trazabilidad e inviolabilidad** de los registros que documenten las entregas de bienes y prestaciones de servicios, mediante el uso de:

* Firma eletrónica
* Código QR
* Blockchain

La biblioteca Batuz permite mediante su componente TicketBAI:

* [Emitir facturas o justificantes de venta en PDF](https://github.com/mdiago/Batuz/wiki/TicketBAI---00001-Ejemplo-emisi%C3%B3n-factura-en-PDF) según las especificaciones
* Emitir documentos xml TicketBAI
* Firmar documentos xml TicketBAI según las especificaciones
* Validar la firma de documentos TicketBAI según las especificaciones
* Validar la secuencia Blockchain de una serie de documentos xml de facturas o justificantes firmados.

Para realizar estas operaciones, partiremos del objeto Batuz.Negocio.Documento.Documento, el cual utilizaremos como parámetro de entrada para la generación de los pdf de factura, documentos xml ...

![image](https://user-images.githubusercontent.com/22330809/136928401-4c652a4f-0f7c-435e-94f2-04b933866b24.png)

En el proyecto Batuz.Test que encontramos en el repositorio podemos encontrar un ejemplo de generación de pdf de factura mediante el cual obtendremos el siguiente resultado:

![image](https://user-images.githubusercontent.com/22330809/136931933-eb79e6b4-57f5-4c91-903e-af0e05a08bef.png)

Editando la plantilla html utilizada para la emisión de la factura, podemos personalizar nuestras facturas y justificantes de venta con el formato y los datos que deseemos.




