﻿using System;
namespace RccManager.Domain.Entities;

public abstract class BaseEntity
{
	public Guid Id { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public bool Active { get; set; }
    private DateTime? _createdAt;

    public DateTime? CreatedAt
    {
        get { return _createdAt; }
        set { _createdAt = (value == null ? Helpers.Helpers.DateTimeNow() : value); }
    }
}

