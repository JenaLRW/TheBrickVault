using System;

public class RebrickableService
{
    private readonly HttpClient _httpClient;

    public RebrickableService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

}
