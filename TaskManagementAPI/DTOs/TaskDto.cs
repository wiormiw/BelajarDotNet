namespace TaskManagementAPI.DTOs;

public record TaskResponseDto(int Id, string Title, string? Description, bool isCompleted);
public record TaskRequestDto(string Title, string? Description);
public record TaskUpdateDto(string? Title, string? Description, bool? IsCompleted);
