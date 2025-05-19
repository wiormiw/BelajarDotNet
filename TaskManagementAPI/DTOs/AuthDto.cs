namespace TaskManagementAPI.DTOs;

public record AuthRequestDto(string Username, string Password);
public record AuthResponseDto(string Token);