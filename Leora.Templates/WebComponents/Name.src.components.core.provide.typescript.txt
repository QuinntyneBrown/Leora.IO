export var provide
    = (app: any, service: any) => app.service((service as any).serviceName, [...service.$inject, service])