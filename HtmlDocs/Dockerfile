FROM nginx:alpine

WORKDIR /usr/share/nginx/html

COPY HtmlDocs/nginx.conf /etc/nginx/nginx.conf

COPY ../SysML2.NET.CodeGenerator.Tests/bin/Debug/net9.0/_SysML2.NET.Core.AutoGenHtmlDocs/index.html  .
COPY ../SysML2.NET.CodeGenerator.Tests/bin/Debug/net9.0/_SysML2.NET.Core.AutoGenHtmlDocs/sysml2-class-inheritance.svg  .
COPY ../SysML2.NET.CodeGenerator/Resources/HtmlDocs .